using Complaints.Worker.Interfaces;
using Microsoft.Extensions.Hosting;

namespace Complaints.Worker
{
    public class ComplaintWorkerService : BackgroundService
    {
        private readonly IRabbitMqConsumer _consumer;

        public ComplaintWorkerService(IRabbitMqConsumer consumer)
        {
            _consumer = consumer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _consumer.ConsumeAsync(stoppingToken);
        }
    }
}
