using System.Collections.Generic;
using System.Threading.Tasks;
using Bakana.Core.Entities;

namespace Bakana.Core.Repositories
{
    public interface IStepRepository
    {
        Task<ulong> Create(Step step);
        Task Delete(ulong stepId);
        Task<Step> Get(ulong stepId);

        Task CreateOrUpdateStepArtifact(StepArtifact artifact);
        Task CreateOrUpdateStepVariable(StepVariable variable);
        Task CreateOrUpdateStepOption(StepOption option);
        Task<List<Step>> GetAll(string batchId);
    }
}