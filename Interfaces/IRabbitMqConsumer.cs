using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complaints.Worker.Interfaces
{
    public interface IRabbitMqConsumer
    {
        Task ConsumeAsync<T>(string queueName, Func<T, Task> onMessage, CancellationToken cancellationToken);
    }
}
