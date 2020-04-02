using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Bakana.Core.Entities;
using Bakana.Core.Extensions;
using ServiceStack.OrmLite;

namespace Bakana.Core.Repositories
{
    public static class StepExtensions
    {
        internal static async Task CreateSteps(this IDbConnection db, IEnumerable<Step> steps)
        {
            if (steps == null) return;

            await steps.Iter(db.CreateStep);
        }

        internal static async Task<ulong> CreateStep(this IDbConnection db, Step step)
        {
            await db.SaveAsync(step, true);

            await db.CreateOrUpdateStepArtifacts(step.InputArtifacts);
            await db.CreateOrUpdateStepArtifacts(step.OutputArtifacts);
            await db.CreateOrUpdateStepVariables(step.Variables);
            await db.CreateOrUpdateStepOptions(step.Options);
            await db.CreateCommands(step.Commands);

            return step.Id;
        }
        
        internal static async Task<ulong> UpdateStep(this IDbConnection db, Step step)
        {
            await db.SaveAsync(step, true);

            await db.CreateOrUpdateStepArtifacts(step.InputArtifacts);
            await db.CreateOrUpdateStepArtifacts(step.OutputArtifacts);
            await db.CreateOrUpdateStepVariables(step.Variables);
            await db.CreateOrUpdateStepOptions(step.Options);
            await db.CreateCommands(step.Commands);

            return step.Id;
        }
        
        internal static async Task<ulong> GetStepPkByStepId(this IDbConnection db, string batchId, string stepId)
        {
            var q = db
                .From<Step>()
                .Where(c => c.StepId == stepId && c.BatchId == batchId)
                .Select(c => c.Id);

            return await db.ScalarAsync<ulong>(q);
        }


        internal static async Task CreateOrUpdateStepArtifacts(this IDbConnection db, IEnumerable<StepArtifact> artifacts)
        {
            if (artifacts == null) return;

            await artifacts.Iter(db.CreateOrUpdateStepArtifact);
        }
        
        internal static async Task CreateOrUpdateStepArtifact(this IDbConnection db, StepArtifact artifact)
        {
            await db.SaveAsync(artifact, true);
        }
        
        internal static async Task CreateOrUpdateStepOptions(this IDbConnection db, IEnumerable<StepOption> options)
        {
            if (options == null) return;

            await options.Iter(db.CreateOrUpdateStepOption);
        }

        internal static async Task CreateOrUpdateStepOption(this IDbConnection db, StepOption option)
        {
            await db.SaveAsync(option, true);
        }
        
        internal static async Task CreateOrUpdateStepVariables(this IDbConnection db, IEnumerable<StepVariable> variables)
        {
            if (variables == null) return;

            await variables.Iter(db.CreateOrUpdateStepVariable);
        }

        internal static async Task CreateOrUpdateStepVariable(this IDbConnection db, StepVariable variable)
        {
            await db.SaveAsync(variable, true);
        }
        
        internal static async Task<Step> GetStep(this IDbConnection db, ulong id)
        {
            var step = await db.LoadSingleByIdAsync<Step>(id, 
                new []{ nameof(Step.Options), nameof(Step.Variables)});
            
            step.InputArtifacts = await db.GetAllInputStepArtifacts(step.Id);
            step.OutputArtifacts = await db.GetAllOutputStepArtifacts(step.Id);
            step.Commands = await db.GetAllCommands(step.Id);

            return step;
        }

        internal static async Task<List<Step>> GetAllSteps(this IDbConnection db, string batchId)
        {
            var steps = await db.LoadSelectAsync<Step>(
                s => s.BatchId == batchId, 
                new []{ nameof(Step.Options), nameof(Step.Variables)});
            
            await steps.Iter(async step =>
            {
                step.InputArtifacts = await db.GetAllInputStepArtifacts(step.Id);
                step.OutputArtifacts = await db.GetAllOutputStepArtifacts(step.Id);
                step.Commands = await db.GetAllCommands(step.Id);
            });

            return steps;
        }
    }
}