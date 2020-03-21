using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Bacan.Batching
{
    public class BatchStateManager : BackgroundService
    {
        private readonly IHostApplicationLifetime hostApplicationLifetime;
        private readonly IBatch batch;
        private readonly IJobStateClient jobStateClient;

        public BatchStateManager(
            IHostApplicationLifetime hostApplicationLifetime,
            IBatch batch,
            IJobStateClient jobStateClient)
        {
            this.hostApplicationLifetime = hostApplicationLifetime;
            this.batch = batch;
            this.jobStateClient = jobStateClient;
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested && !batch.IsCompleted)
            {
                var jobResponse = jobStateClient.Get();
                if (jobResponse == null)
                {
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                    continue;
                }
                
                batch.Update(jobResponse.Id, jobResponse.State);
            }
            
            if (batch.IsCompleted)
                hostApplicationLifetime.StopApplication();
        }
    }
}