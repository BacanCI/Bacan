namespace Bakana.WorkerLogs
{
    public interface IWorkerLogContext
    {
        string QueueName { get; }
    }
}