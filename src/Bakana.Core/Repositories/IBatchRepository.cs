using System.Collections.Generic;
using System.Threading.Tasks;
using Bakana.Core.Entities;

namespace Bakana.Core.Repositories
{
    public interface IBatchRepository
    {
        Task Create(Batch batch);
        Task<bool> Update(Batch batch);
        Task<bool> Delete(string batchId);
        Task<Batch> Get(string batchId);
        Task UpdateState(string batchId, BatchState state);
        Task<bool> DoesBatchExist(string batchId);
        
        Task<ulong> CreateOrUpdateBatchVariable(BatchVariable variable);
        Task<BatchVariable> GetBatchVariable(ulong id);
        Task<BatchVariable> GetBatchVariable(string batchId, string variableName);
        Task<List<BatchVariable>> GetAllBatchVariables(string batchId);
        Task<bool> DeleteBatchVariable(ulong id);
        Task<bool> DoesBatchVariableExist(string batchId, string variableName);
        
        Task<ulong> CreateOrUpdateBatchOption(BatchOption option);
        Task<BatchOption> GetBatchOption(ulong id);
        Task<BatchOption> GetBatchOption(string batchId, string optionName);
        Task<List<BatchOption>> GetAllBatchOptions(string batchId);
        Task<bool> DeleteBatchOption(ulong id);
        Task<bool> DoesBatchOptionExist(string batchId, string optionName);
        
        Task<ulong> CreateBatchArtifact(BatchArtifact batchArtifact);
        Task UpdateBatchArtifact(BatchArtifact batchArtifact);
        Task<BatchArtifact> GetBatchArtifact(ulong id);
        Task<BatchArtifact> GetBatchArtifact(string batchId, string artifactName);
        Task<List<BatchArtifact>> GetAllBatchArtifacts(string batchId);
        Task<bool> DeleteBatchArtifact(ulong id);
        Task<bool> DoesBatchArtifactExist(string batchId, string artifactName);
        
        Task<ulong> CreateOrUpdateBatchArtifactOption(BatchArtifactOption option);
        Task<BatchArtifactOption> GetBatchArtifactOption(ulong id);
        Task<BatchArtifactOption> GetBatchArtifactOption(ulong artifactName, string optionName);
        Task<List<BatchArtifactOption>> GetAllBatchArtifactOptions(ulong artifactName);
        Task<bool> DeleteBatchArtifactOption(ulong id);
        Task<bool> DoesBatchArtifactOptionExist(string batchId, string artifactName, string optionName);
    }
}