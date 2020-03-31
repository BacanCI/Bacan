using System.Collections.Generic;
using System.Threading.Tasks;
using Bakana.Core.Entities;

namespace Bakana.Core.Repositories
{
    public interface IStepRepository
    {
        Task CreateOrUpdate(Step step);
        Task Delete(string stepId);
        Task<Step> Get(string stepId);

        Task CreateOrUpdateStepArtifact(StepArtifact artifact);
        Task CreateOrUpdateStepVariable(StepVariable variable);
        Task CreateOrUpdateStepOption(StepOption option);
        Task<List<Step>> GetAll(string batchId);
    }
}