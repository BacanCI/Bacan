namespace Bacan.WorkerLogs
{
    public class WorkerLogContext : IWorkerLogContext
    {
        public WorkerLogContext(string queueName)
        {
            QueueName = queueName;
        }
        
        public string QueueName { get; }
    }
}