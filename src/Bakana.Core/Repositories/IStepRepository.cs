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
        Task<Step> Get(string batchId, string stepName);
        Task<List<Step>> GetAll(string batchId);
        Task UpdateState(ulong id, StepState state);
        Task<bool> DoesStepExist(string batchId, string stepName);
        
        Task<ulong> CreateOrUpdateStepVariable(StepVariable variable);
        Task<StepVariable> GetStepVariable(ulong id);
        Task<StepVariable> GetStepVariable(ulong stepId, string variableName);
        Task<List<StepVariable>> GetAllStepVariables(ulong stepId);
        Task DeleteStepVariable(ulong id);
        Task<bool> DoesStepVariableExist(string batchId, string stepName, string variableName);
        
        Task<ulong> CreateOrUpdateStepOption(StepOption option);
        Task<StepOption> GetStepOption(ulong id);
        Task<StepOption> GetStepOption(ulong stepId, string optionName);
        Task<List<StepOption>> GetAllStepOptions(ulong stepId);
        Task DeleteStepOption(ulong id);
        Task<bool> DoesStepOptionExist(string batchId, string stepName, string optionName);
        
        Task<ulong> CreateStepArtifact(StepArtifact stepArtifact);
        Task UpdateStepArtifact(StepArtifact stepArtifact);
        Task<StepArtifact> GetStepArtifact(ulong id);
        Task<StepArtifact> GetStepArtifact(ulong stepId, string artifactName);
        Task<List<StepArtifact>> GetAllStepArtifacts(ulong stepId);
        Task DeleteStepArtifact(ulong id);
        Task<bool> DoesStepArtifactExist(string batchId, string stepName, string artifactName);

        
        Task<ulong> CreateOrUpdateStepArtifactOption(StepArtifactOption option);
        Task<StepArtifactOption> GetStepArtifactOption(ulong id);
        Task<StepArtifactOption> GetStepArtifactOption(ulong stepArtifactId, string optionName);
        Task<List<StepArtifactOption>> GetAllStepArtifactOptions(ulong stepArtifactId);
        Task<bool> DeleteStepArtifactOption(ulong id);
        Task<bool> DoesStepArtifactOptionExist(string batchId, string stepName, string artifactName, string optionName);
    }
}