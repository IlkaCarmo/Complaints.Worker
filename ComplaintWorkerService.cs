using Complaints.Worker.Interfaces;
using Complaints.Worker.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Complaints.Worker
{
    public class ComplaintWorkerService : BackgroundService
    {
        private readonly ILogger<ComplaintWorkerService> _logger;
        private readonly IRabbitMqConsumer _consumer;
        private readonly IComplaintClassifier _classifier;
        private readonly IComplaintRepository _repository;
        private readonly ICategoryProvider _categoryProvider;

        public ComplaintWorkerService(
            ILogger<ComplaintWorkerService> logger,
            IRabbitMqConsumer consumer,
            IComplaintClassifier classifier,
            IComplaintRepository repository,
            ICategoryProvider categoryProvider)
        {
            _logger = logger;
            _consumer = consumer;
            _classifier = classifier;
            _repository = repository;
            _categoryProvider = categoryProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker iniciado em {time}", DateTimeOffset.Now);

            await _consumer.ConsumeAsync<ComplaintDto>("complaints-queue", async dto =>
            {
                _logger.LogInformation("Processando reclamação de {Customer}", dto.CustomerName);

                dto.Deadline = dto.CreatedAt.AddDays(10);
                dto.Status = "Classificada";

                var categorias = _categoryProvider.GetCategorias(); 
                var tags = _classifier.Classify(dto.Description, categorias); 

                dto.Categories = string.Join(",", tags); 

                await _repository.SaveAsync(dto, tags);

                _logger.LogInformation("Reclamação salva com categorias: {Tags}", string.Join(", ", tags));
            }, stoppingToken);

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }

    }
}
