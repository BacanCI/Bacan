using Bakana.Core;

namespace Bakana.Batching
{
    public interface IJob
    {
        string Id { get; set; }
        JobState State { get; set; }
    }
}