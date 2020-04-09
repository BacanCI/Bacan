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

            await db.CreateOrUpdateBatchArtifacts(batch.Artifacts);
            await db.CreateOrUpdateBatchVariables(batch.Variables);
            await db.CreateOrUpdateBatchOptions(batch.Options);
            await db.CreateSteps(batch.Steps);
        }

        internal static async Task<int> UpdateBatch(this IDbConnection db, Batch batch)
        {
            return await db.UpdateAsync(batch);
        }

        internal static async Task<Batch> GetBatch(this IDbConnection db, string batchId)
        {
            var batch = await db.LoadSingleByIdAsync<Batch>(batchId, 
                include: new[] { nameof(Batch.Options), nameof(Batch.Variables) });

            if (batch == null) return null;

            batch.Artifacts = await db.LoadSelectAsync<BatchArtifact>(artifact => artifact.BatchId == batchId);
            batch.Steps = await db.GetAllSteps(batchId);

            return batch;
        }

        internal static async Task<int> UpdateBatchState(this IDbConnection db, string batchId, BatchState state)
        {
            return await db.UpdateOnlyAsync(() => new Batch { State = state }, where: p => p.Id == batchId);
        }

        internal static async Task<bool> DoesBatchExist(this IDbConnection db, string batchId)
        {
            return await db.ExistsAsync<Batch>(b => b.Id == batchId);
        }

        internal static async Task CreateOrUpdateBatchOptions(this IDbConnection db, IEnumerable<BatchOption> options)
        {
            await options.Iter(db.CreateOrUpdateBatchOption);
        }

        internal static async Task<ulong> CreateOrUpdateBatchOption(this IDbConnection db, BatchOption option)
        {
            await db.SaveAsync(option, true);
            return option.Id;
        }

        internal static async Task<BatchOption> GetBatchOption(this IDbConnection db, ulong id)
        {
            return await db.LoadSingleByIdAsync<BatchOption>(id);
        }

        internal static async Task<List<BatchOption>> GetAllBatchOptions(this IDbConnection db, string batchId)
        {
            return await db.LoadSelectAsync<BatchOption>(c => c.BatchId == batchId);
        }

        internal static async Task<ulong> GetBatchOptionPkByOptionId(this IDbConnection db, string batchId, string optionId)
        {
            var q = db
                .From<BatchOption>()
                .Where(c => c.OptionId == optionId && c.BatchId == batchId)
                .Select(c => c.Id);

            return await db.ScalarAsync<ulong>(q);
        }

        internal static async Task<bool> DoesBatchOptionExist(this IDbConnection db, string batchId, string optionId)
        {
            return await db.ExistsAsync<BatchOption>(b => b.BatchId == batchId && b.OptionId == optionId);
        }

        internal static async Task CreateOrUpdateBatchVariables(this IDbConnection db, IEnumerable<BatchVariable> variables)
        {
            await variables.Iter(db.CreateOrUpdateBatchVariable);
        }

        internal static async Task<bool> CreateOrUpdateBatchVariable(this IDbConnection db, BatchVariable variable)
        {
            return await db.SaveAsync(variable, true);
        }

        internal static async Task<BatchVariable> GetBatchVariable(this IDbConnection db, ulong id)
        {
            return await db.LoadSingleByIdAsync<BatchVariable>(id);
        }

        internal static async Task<List<BatchVariable>> GetAllBatchVariables(this IDbConnection db, string batchId)
        {
            return await db.LoadSelectAsync<BatchVariable>(c => c.BatchId == batchId);
        }

        internal static async Task<ulong> GetBatchVariablePkByVariableId(this IDbConnection db, string batchId, string variableId)
        {
            var q = db
                .From<BatchVariable>()
                .Where(c => c.VariableId == variableId && c.BatchId == batchId)
                .Select(c => c.Id);

            return await db.ScalarAsync<ulong>(q);
        }

        internal static async Task<bool> DoesBatchVariableExist(this IDbConnection db, string batchId, string variableId)
        {
            return await db.ExistsAsync<BatchVariable>(b => b.BatchId == batchId && b.VariableId == variableId);
        }

        internal static async Task CreateOrUpdateBatchArtifacts(this IDbConnection db, IEnumerable<BatchArtifact> artifacts)
        {
            await artifacts.Iter(db.CreateOrUpdateBatchArtifact);
        }

        internal static async Task CreateOrUpdateBatchArtifact(this IDbConnection db, BatchArtifact artifact)
        {
            await db.SaveAsync(artifact, true);
        }

        internal static async Task<int> UpdateBatchArtifact(this IDbConnection db, BatchArtifact artifact)
        {
            return await db.UpdateAsync(artifact);
        }

        internal static async Task<ulong> GetBatchArtifactPkByArtifactId(this IDbConnection db, string batchId, string artifactId)
        {
            var q = db
                .From<BatchArtifact>()
                .Where(c => c.ArtifactId == artifactId && c.BatchId == batchId)
                .Select(c => c.Id);

            return await db.ScalarAsync<ulong>(q);
        }

        internal static async Task<BatchArtifact> GetBatchArtifact(this IDbConnection db, ulong id)
        {
            return await db.LoadSingleByIdAsync<BatchArtifact>(id);
        }

        internal static async Task<List<BatchArtifact>> GetAllBatchArtifacts(this IDbConnection db, string batchId)
        {
            return await db.LoadSelectAsync<BatchArtifact>(a => a.BatchId == batchId);
        }

        internal static async Task<bool> DoesBatchArtifactExist(this IDbConnection db, string batchId, string artifactId)
        {
            return await db.ExistsAsync<BatchArtifact>(b => b.BatchId == batchId && b.ArtifactId == artifactId);
        }

        internal static async Task<ulong> CreateOrUpdateBatchArtifactOption(this IDbConnection db, BatchArtifactOption option)
        {
            await db.SaveAsync(option, true);

            return option.Id;
        }

        internal static async Task<BatchArtifactOption> GetBatchArtifactOption(this IDbConnection db, ulong id)
        {
            return await db.LoadSingleByIdAsync<BatchArtifactOption>(id);
        }

        internal static async Task<List<BatchArtifactOption>> GetAllBatchArtifactOptions(this IDbConnection db, ulong batchArtifactId)
        {
            return await db.LoadSelectAsync<BatchArtifactOption>(c => c.BatchArtifactId == batchArtifactId);
        }

        internal static async Task<ulong> GetBatchArtifactOptionPkByOptionId(this IDbConnection db, ulong batchArtifactId, string optionId)
        {
            var q = db
                .From<BatchArtifactOption>()
                .Where(c => c.OptionId == optionId && c.BatchArtifactId == batchArtifactId)
                .Select(c => c.Id);

            return await db.ScalarAsync<ulong>(q);
        }

        internal static async Task<bool> DoesBatchArtifactOptionExist(this IDbConnection db, string batchId, string artifactId, string optionId)
        {
            var id = await db.GetBatchArtifactPkByArtifactId(batchId, artifactId);
            var batchArtifact = await db.GetBatchArtifact(id);

            if (batchArtifact != null)
                return await db.ExistsAsync<BatchArtifactOption>(o => o.BatchArtifactId == batchArtifact.Id && o.OptionId == optionId);

            return false;
        }
    }
}