using Complaints.Worker.Interfaces;
using Complaints.Worker.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complaints.Worker.Services
{
    public class RabbitMqConsumer : IRabbitMqConsumer
    {
        private readonly IComplaintClassifier _classifier;
        private readonly IComplaintRepository _repository;
        private readonly ICategoryProvider _categoryProvider;

        public RabbitMqConsumer(IComplaintClassifier classifier, IComplaintRepository repository, ICategoryProvider categoryProvider)
        {
            _classifier = classifier;
            _repository = repository;
            _categoryProvider = categoryProvider;
        }

        public async Task ConsumeAsync(CancellationToken cancellationToken)
        {

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "complaints", durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);
                var complaint = JsonConvert.DeserializeObject<ComplaintDto>(json);
                
                complaint.Deadline = DateTime.UtcNow.AddDays(10);
                complaint.Status = "Classificada";

                var categorias = _categoryProvider.GetCategorias();
                var tags = _classifier.Classify(complaint.Description, categorias);

                await _repository.SaveAsync(complaint, tags);
            };

            channel.BasicConsume(queue: "complaints", autoAck: true, consumer: consumer);

            while (!cancellationToken.IsCancellationRequested)
                await Task.Delay(1000);
        }

    }
}
