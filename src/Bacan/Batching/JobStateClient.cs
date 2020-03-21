using Bacan.Core;
using ServiceStack.Messaging;

namespace Bacan.Batching
{
    public class JobStateClient : IJobStateClient
    {
        private readonly IMessageQueueClient messageQueueClient;
        private readonly IJobStateContext jobStateContext;

        public JobStateClient(
            IMessageQueueClient messageQueueClient,
            IJobStateContext jobStateContext)
        {
            this.messageQueueClient = messageQueueClient;
            this.jobStateContext = jobStateContext;
        }

        public GetJobResponse Get()
        {
            return messageQueueClient.GetAsync<GetJobResponse>(jobStateContext.QueueName)?.GetBody();
        }
    }
}