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

        public async Task ConsumeAsync<T>(string queueName, Func<T, Task> onMessage, CancellationToken cancellationToken)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            //cria consumidor
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);
                var data = JsonConvert.DeserializeObject<T>(json);

                if (data != null)
                    await onMessage(data);
            };

            channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

            while (!cancellationToken.IsCancellationRequested)
                await Task.Delay(1000, cancellationToken);
        }

    }
}
