using Bakana.Core;

namespace Bakana.Batching
{
    public interface IJobStateClient
    {
        GetJobResponse Get();
    }
}