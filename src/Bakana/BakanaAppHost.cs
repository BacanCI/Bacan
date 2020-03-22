using System;
using Bakana.Batching;
using Bakana.Core;
using Bakana.ServiceInterface;
using Bakana.WorkerLogs;
using Funq;
using Microsoft.Extensions.Hosting;
using ServiceStack;
using ServiceStack.Messaging;
using ServiceStack.Messaging.Redis;
using ServiceStack.Redis;

namespace Bakana
{
    public class BakanaAppHost : AppHostBase
    {
        private readonly IHostEnvironment hostEnvironment;
        private readonly IHostApplicationLifetime hostApplicationLifetime;

        public BakanaAppHost(IHostEnvironment hostEnvironment, IHostApplicationLifetime hostApplicationLifetime) : base("Bakana", typeof(BakanaServices).Assembly)
        {
            this.hostEnvironment = hostEnvironment;
            this.hostApplicationLifetime = hostApplicationLifetime;
        }

        public override void Configure(Container container)
        {
            var redisFactory = new PooledRedisClientManager(
                AppSettings.GetString(AppSettingsKeys.RedisConnection));
            var mqServer = new RedisMqServer(redisFactory, retryCount:AppSettings.Get<int>(AppSettingsKeys.RedisRetries));
            var mqClient = mqServer.CreateMessageQueueClient();
            
            var jobStateContext = new JobStateContext("jobState");
            container.AddSingleton<IJobStateClient>(() => new JobStateClient(mqClient, jobStateContext));
            
            var workerLogContext = new WorkerLogContext("workerLog");
            container.AddSingleton<IWorkerLogClient>(() => new WorkerLogClient(mqClient, workerLogContext));

            container.AddSingleton<IBatch, Batch>();

            
            var jobGroupId = AppSettings.GetString(AppSettingsKeys.JobGroupId); 
            var numberOfJobs = AppSettings.Get<int>(AppSettingsKeys.NumberOfJobs);

            AfterInitCallbacks.Add(host =>
            {
                mqServer.Start();
                
                for (var i = 1; i <= numberOfJobs; i++)
                {
                    var job = new JobRequest
                    {
                        JobId = Guid.NewGuid().ToString(),
                        GroupId = jobGroupId,
                        Description = $"Job {i}"
                    };

                    var message = new Message<JobRequest>(job)
                    {
                        ReplyTo = "jobState",
                    };
                    mqClient.Publish(message);
                }

                // var serviceGateway = HostContext.AppHost.GetServiceGateway();
                // serviceGateway.Send(batch);
            });
        }
    }
}