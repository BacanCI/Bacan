using Bacan.Core;
using ServiceStack.Messaging;

namespace Bacan.WorkerLogs
{
    public class WorkerLogClient : IWorkerLogClient
    {
        private readonly IMessageQueueClient messageQueueClient;
        private readonly IWorkerLogContext workerLogContext;

        public WorkerLogClient(
            IMessageQueueClient messageQueueClient,
            IWorkerLogContext workerLogContext)
        {
            this.messageQueueClient = messageQueueClient;
            this.workerLogContext = workerLogContext;
        }
        
        public string Get()
        {
            return messageQueueClient.GetAsync<WorkerLogEntry>(workerLogContext.QueueName)?.GetBody().Log;
        }
    }
}