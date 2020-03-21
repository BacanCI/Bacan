using Bacan.Core;

namespace Bacan.Batching
{
    public interface IJobStateClient
    {
        GetJobResponse Get();
    }
}