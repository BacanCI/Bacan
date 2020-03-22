namespace Bakana.Batching
{
    public interface IJobStateContext
    {
        string QueueName { get; }
    }
}