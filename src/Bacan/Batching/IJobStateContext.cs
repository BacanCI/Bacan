namespace Bacan.Batching
{
    public interface IJobStateContext
    {
        string QueueName { get; }
    }
}