using Complaints.Worker;
using Complaints.Worker.Interfaces;
using Complaints.Worker.Persistence;
using Complaints.Worker.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.AddHostedService<ComplaintWorkerService>();

                services.AddScoped<IRabbitMqConsumer, RabbitMqConsumer>();
                services.AddScoped<IComplaintClassifier, ComplaintClassifier>();
                services.AddScoped<IComplaintRepository, ComplaintRepository>();

                services.AddDbContext<ComplaintsDbContext>(options =>
                 options.UseMySql("server=localhost;database=complaintsdb;user=;password=",
                 new MySqlServerVersion(new Version(8, 0, 34))));
            })
            .Build();

        await host.RunAsync();
    }
}
