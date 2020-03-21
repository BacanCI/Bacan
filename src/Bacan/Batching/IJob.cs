using Bacan.Core;

namespace Bacan.Batching
{
    public interface IJob
    {
        string Id { get; set; }
        JobState State { get; set; }
    }
}