using System.Collections.Generic;
using Bacan.Core;
using Bacan.ServiceInterface;
using Funq;
using Microsoft.Extensions.Hosting;
using ServiceStack;
using ServiceStack.Messaging.Redis;
using ServiceStack.Redis;

namespace Bacan
{
    public class BacanAppHost : AppHostBase
    {
        private readonly IHostEnvironment hostEnvironment;
        private readonly IHostApplicationLifetime hostApplicationLifetime;

        public BacanAppHost(IHostEnvironment hostEnvironment, IHostApplicationLifetime hostApplicationLifetime) : base("Bacan", typeof(BacanServices).Assembly)
        {
            this.hostEnvironment = hostEnvironment;
            this.hostApplicationLifetime = hostApplicationLifetime;
        }

        public override void Configure(Container container)
        {
            var redisFactory = new PooledRedisClientManager(
                AppSettings.GetString(AppSettingsKeys.RedisConnection));
            var mqServer = new RedisMqServer(redisFactory, retryCount:AppSettings.Get<int>(AppSettingsKeys.RedisRetries));
            //var mqClient = mqServer.CreateMessageQueueClient();

            var jobGroupId = AppSettings.GetString(AppSettingsKeys.JobGroupId); 
            var numberOfJobs = AppSettings.Get<int>(AppSettingsKeys.NumberOfJobs);

            AfterInitCallbacks.Add(host =>
            {
                mqServer.Start();
                
                var batch = new CreateBatch
                {
                    Id = "123",
                    Jobs = new List<CreateBatchJob>
                    {
                        new CreateBatchJob
                        {
                            Id = "123-1",
                            Description = "Do something"
                        },
                        new CreateBatchJob
                        {
                            Id = "123-2",
                            Description = "Do something else"
                        },
                    }
                };

                var serviceGateway = HostContext.AppHost.GetServiceGateway();
                serviceGateway.Send(batch);
            });
        }
    }
}