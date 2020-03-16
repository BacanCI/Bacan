namespace Bacan.Core
{
    public static class QueueNames
    {
        public static string JobState(string batchId) => $"Batch:{batchId}:JobState";
        public static string LogCollector(string batchId) => $"Batch:{batchId}:Log";
    }
}
