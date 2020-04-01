using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Bakana.Core.Entities;
using Bakana.Core.Extensions;
using ServiceStack.OrmLite;

namespace Bakana.Core.Repositories
{
    public static class BatchExtensions
    {
        internal static async Task CreateBatch(this IDbConnection db, Batch batch)
        {
            await db.SaveAsync(batch, true);

            await db.CreateOrUpdateBatchArtifacts(batch.InputArtifacts);
            await db.CreateOrUpdateBatchVariables(batch.Variables);
            await db.CreateOrUpdateBatchOptions(batch.Options);
            await db.CreateSteps(batch.Steps);
        }
        
        internal static async Task UpdateBatch(this IDbConnection db, Batch batch)
        {
            await db.UpdateAsync(batch);
        }

        internal static async Task CreateOrUpdateBatchArtifacts(this IDbConnection db, IEnumerable<BatchArtifact> artifacts)
        {
            if (artifacts == null) return;

            await artifacts.Iter(db.CreateOrUpdateBatchArtifact);
        }
        
        internal static async Task CreateOrUpdateBatchArtifact(this IDbConnection db, BatchArtifact artifact)
        {
            await db.SaveAsync(artifact, true);
        }
        
        internal static async Task CreateOrUpdateBatchOptions(this IDbConnection db, IEnumerable<BatchOption> options)
        {
            if (options == null) return;

            await options.Iter(db.CreateOrUpdateBatchOption);
        }

        internal static async Task CreateOrUpdateBatchOption(this IDbConnection db, BatchOption option)
        {
            await db.SaveAsync(option, true);
        }
        
        internal static async Task CreateOrUpdateBatchVariables(this IDbConnection db, IEnumerable<BatchVariable> variables)
        {
            if (variables == null) return;

            await variables.Iter(db.CreateOrUpdateBatchVariable);
        }

        internal static async Task CreateOrUpdateBatchVariable(this IDbConnection db, BatchVariable variable)
        {
            await db.SaveAsync(variable, true);
        }
        
        internal static async Task<Batch> GetBatch(this IDbConnection db, string batchId)
        {
            var batch = await db.LoadSingleByIdAsync<Batch>(
                batchId, include: 
                new[] { nameof(Batch.Options), nameof(Batch.Variables) });
                
            batch.InputArtifacts = await db.LoadSelectAsync<BatchArtifact>(artifact => artifact.BatchId == batchId);
            batch.Steps = await db.GetAllSteps(batchId);

            return batch;
        }
    }
}