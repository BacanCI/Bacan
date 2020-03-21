using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Bacan.WorkerLogs
{
    public class WorkerLogCollector : BackgroundService
    {
        private readonly IWorkerLogClient logClient;

        public WorkerLogCollector(IWorkerLogClient logClient)
        {
            this.logClient = logClient;
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var log = logClient.Get();
                if (log == null)
                {
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                    continue;
                }
                
                Console.WriteLine(log);
            }
        }
    }
}