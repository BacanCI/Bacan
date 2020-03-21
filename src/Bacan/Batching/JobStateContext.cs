namespace Bacan.Batching
{
    public class JobStateContext : IJobStateContext
    {
        public JobStateContext(string queueName)
        {
            QueueName = queueName;
        }
        
        public string QueueName { get; }
    }
}