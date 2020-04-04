using System.Collections.Generic;
using System.Threading.Tasks;
using Bakana.Core.Entities;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Bakana.Core.Repositories
{
    public class BatchRepository : RepositoryBase, IBatchRepository
    {
        public BatchRepository(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
        {
        }
        
        public async Task Create(Batch batch)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                using (var tx = db.OpenTransaction())
                {
                    await db.CreateBatch(batch);

                    tx.Commit();
                }
            }
        }

        public async Task<bool> Update(Batch batch)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                var rowsUpdated = await db.UpdateBatch(batch);
                return rowsUpdated > 0;
            }
        }

        public async Task<bool> Delete(string batchId)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                var rowsDeleted = await db.DeleteByIdAsync<Batch>(batchId);
                return rowsDeleted > 0;
            }
        }

        public async Task<Batch> Get(string batchId)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                return await db.GetBatch(batchId);
            }
        }

        public async Task UpdateState(string batchId, BatchState state)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                await db.UpdateBatchState(batchId, state);
            }
        }

        public async Task<bool> DoesExist(string batchId)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                return await db.DoesExist(batchId);
            }
        }

        public async Task<ulong> CreateOrUpdateBatchVariable(BatchVariable variable)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                await db.CreateOrUpdateBatchVariable(variable);
                
                return variable.Id;
            }
        }

        public async Task<BatchVariable> GetBatchVariable(ulong id)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                return await db.GetBatchVariable(id);
            }
        }

        public async Task<BatchVariable> GetBatchVariable(string batchId, string variableId)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                var id = await db.GetBatchVariablePkByVariableId(batchId, variableId);
                return await db.GetBatchVariable(id);
            }
        }

        public async Task<List<BatchVariable>> GetAllBatchVariables(string batchId)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                return await db.GetAllBatchVariables(batchId);
            }
        }

        public async Task<bool> DeleteBatchVariable(ulong id)
        {
            var rowsDeleted = await DeleteByIdAsync<BatchVariable>(id);
            return rowsDeleted > 0;
        }

        public async Task<bool> DoesBatchVariableExist(string batchId, string variableId)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                return await db.DoesBatchVariableExist(batchId, variableId);
            }
        }

        public async Task<ulong> CreateOrUpdateBatchOption(BatchOption option)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                return await db.CreateOrUpdateBatchOption(option);
            }
        }

        public async Task<BatchOption> GetBatchOption(ulong id)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                return await db.GetBatchOption(id);
            }
        }

        public async Task<BatchOption> GetBatchOption(string batchId, string optionId)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                var id = await db.GetBatchOptionPkByOptionId(batchId, optionId);
                return await db.GetBatchOption(id);
            }
        }

        public async Task<List<BatchOption>> GetAllBatchOptions(string batchId)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                return await db.GetAllBatchOptions(batchId);
            }
        }

        public async Task<bool> DeleteBatchOption(ulong id)
        {
            var rowsDeleted = await DeleteByIdAsync<BatchOption>(id);
            return rowsDeleted > 0;
        }

        public async Task<bool> DoesBatchOptionExist(string batchId, string optionId)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                return await db.DoesBatchOptionExist(batchId, optionId);
            }
        }

        public async Task<ulong> CreateBatchArtifact(BatchArtifact batchArtifact)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                await db.CreateOrUpdateBatchArtifact(batchArtifact);
                
                return batchArtifact.Id;
            }
        }

        public async Task UpdateBatchArtifact(BatchArtifact batchArtifact)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                await db.UpdateBatchArtifact(batchArtifact);
            }
        }

        public async Task<BatchArtifact> GetBatchArtifact(ulong id)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                return await db.GetBatchArtifact(id);
            }
        }

        public async Task<BatchArtifact> GetBatchArtifact(string batchId, string artifactId)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                var id = await db.GetBatchArtifactPkByArtifactId(batchId, artifactId);
                return await db.GetBatchArtifact(id);
            }
        }

        public async Task<List<BatchArtifact>> GetAllBatchArtifacts(string batchId)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                return await db.GetAllBatchArtifacts(batchId);
            }
        }

        public async Task<bool> DeleteBatchArtifact(ulong id)
        {
            var rowsDeleted = await DeleteByIdAsync<BatchArtifact>(id);
            return rowsDeleted > 0;
        }

        public async Task<bool> DoesBatchArtifactExist(string batchId, string artifactId)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                return await db.DoesBatchArtifactExist(batchId, artifactId);
            }
        }

        public async Task<ulong> CreateOrUpdateBatchArtifactOption(BatchArtifactOption option)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                return await db.CreateOrUpdateBatchArtifactOption(option);
            }
        }

        public async Task<BatchArtifactOption> GetBatchArtifactOption(ulong id)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                return await db.GetBatchArtifactOption(id);
            }
        }

        public async Task<BatchArtifactOption> GetBatchArtifactOption(ulong artifactId, string optionId)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                var id = await db.GetBatchArtifactOptionPkByOptionId(artifactId, optionId);
                return await db.GetBatchArtifactOption(id);
            }
        }

        public async Task<List<BatchArtifactOption>> GetAllBatchArtifactOptions(ulong artifactId)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                return await db.GetAllBatchArtifactOptions(artifactId);
            }
        }

        public async Task<bool> DeleteBatchArtifactOption(ulong id)
        {
            var rowsDeleted = await DeleteByIdAsync<BatchArtifactOption>(id);
            return rowsDeleted > 0;
        }
        
        public async Task<bool> DoesBatchArtifactOptionExist(string batchId, string artifactId, string optionId)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                return await db.DoesBatchArtifactOptionExist(batchId, artifactId, optionId);
            }
        }
    }
}