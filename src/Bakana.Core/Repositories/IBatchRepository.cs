using System.Collections.Generic;
using System.Threading.Tasks;
using Bakana.Core.Entities;

namespace Bakana.Core.Repositories
{
    public interface IBatchRepository
    {
        Task Create(Batch batch);
        Task Update(Batch batch);
        Task Delete(string batchId);
        Task<Batch> Get(string batchId);
        Task UpdateState(string batchId, BatchState state);
        
        Task<ulong> CreateOrUpdateBatchVariable(BatchVariable variable);
        Task<BatchVariable> GetBatchVariable(ulong id);
        Task<BatchVariable> GetBatchVariable(string batchId, string variableId);
        Task<List<BatchVariable>> GetAllBatchVariables(string batchId);
        Task DeleteBatchVariable(ulong id);
        
        Task<ulong> CreateOrUpdateBatchOption(BatchOption option);
        Task<BatchOption> GetBatchOption(ulong id);
        Task<BatchOption> GetBatchOption(string batchId, string optionId);
        Task<List<BatchOption>> GetAllBatchOptions(string batchId);
        Task DeleteBatchOption(ulong id);
        
        Task<ulong> CreateBatchArtifact(BatchArtifact batchArtifact);
        Task UpdateBatchArtifact(BatchArtifact batchArtifact);
        Task<BatchArtifact> GetBatchArtifact(ulong id);
        Task<BatchArtifact> GetBatchArtifact(string batchId, string artifactId);
        Task<List<BatchArtifact>> GetAllBatchArtifacts(string batchId);
        Task DeleteBatchArtifact(ulong id);
        
        Task<ulong> CreateOrUpdateBatchArtifactOption(BatchArtifactOption option);
        Task<BatchArtifactOption> GetBatchArtifactOption(ulong id);
        Task<BatchArtifactOption> GetBatchArtifactOption(ulong artifactId, string optionId);
        Task<List<BatchArtifactOption>> GetAllBatchArtifactOptions(ulong artifactId);
        Task DeleteBatchArtifactOption(ulong id);
    }
}