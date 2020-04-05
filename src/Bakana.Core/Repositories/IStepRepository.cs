using System.Collections.Generic;
using System.Threading.Tasks;
using Bakana.Core.Entities;

namespace Bakana.Core.Repositories
{
    public interface IStepRepository
    {
        Task<ulong> Create(Step step);
        Task<bool> Update(Step step);
        Task<bool> Delete(ulong stepId);
        Task<Step> Get(ulong stepId);
        Task<Step> Get(string batchId, string stepId);
        Task<List<Step>> GetAll(string batchId);
        Task UpdateState(ulong id, StepState state);
        Task<bool> DoesStepExist(string batchId, string stepId);
        
        Task<ulong> CreateOrUpdateStepVariable(StepVariable variable);
        Task<StepVariable> GetStepVariable(ulong id);
        Task<StepVariable> GetStepVariable(ulong stepId, string variableId);
        Task<List<StepVariable>> GetAllStepVariables(ulong stepId);
        Task DeleteStepVariable(ulong id);
        Task<bool> DoesStepVariableExist(string batchId, string stepId, string variableId);
        
        Task<ulong> CreateOrUpdateStepOption(StepOption option);
        Task<StepOption> GetStepOption(ulong id);
        Task<StepOption> GetStepOption(ulong stepId, string optionId);
        Task<List<StepOption>> GetAllStepOptions(ulong stepId);
        Task DeleteStepOption(ulong id);
        Task<bool> DoesStepOptionExist(string batchId, string stepId, string optionId);
        
        Task<ulong> CreateStepArtifact(StepArtifact stepArtifact);
        Task UpdateStepArtifact(StepArtifact stepArtifact);
        Task<StepArtifact> GetStepArtifact(ulong id);
        Task<StepArtifact> GetStepArtifact(ulong stepId, string artifactId);
        Task<List<StepArtifact>> GetAllStepArtifacts(ulong stepId);
        Task DeleteStepArtifact(ulong id);
        Task<bool> DoesStepArtifactExist(string batchId, string stepId, string artifactId);

        
        Task<ulong> CreateOrUpdateStepArtifactOption(StepArtifactOption option);
        Task<StepArtifactOption> GetStepArtifactOption(ulong id);
        Task<StepArtifactOption> GetStepArtifactOption(ulong artifactId, string optionId);
        Task<List<StepArtifactOption>> GetAllStepArtifactOptions(ulong artifactId);
        Task DeleteStepArtifactOption(ulong id);
        Task<bool> DoesStepArtifactOptionExist(string batchId, string stepId, string artifactId, string optionId);
    }
}