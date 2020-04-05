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

            await db.CreateOrUpdateStepArtifacts(step.Artifacts);
            await db.CreateOrUpdateStepVariables(step.Variables);
            await db.CreateOrUpdateStepOptions(step.Options);
            await db.CreateCommands(step.Commands);

            return step.Id;
        }
        
        internal static async Task UpdateStep(this IDbConnection db, Step step)
        {
            await db.UpdateAsync(step);
        }
        
        internal static async Task<ulong> GetStepPkByStepId(this IDbConnection db, string batchId, string stepId)
        {
            var q = db
                .From<Step>()
                .Where(c => c.StepId == stepId && c.BatchId == batchId)
                .Select(c => c.Id);

            return await db.ScalarAsync<ulong>(q);
        }
        
        internal static async Task<Step> GetStep(this IDbConnection db, ulong id)
        {
            var step = await db.LoadSingleByIdAsync<Step>(id, 
                new []{ nameof(Step.Options), nameof(Step.Variables)});
            
            step.Artifacts = await db.GetAllStepArtifacts(step.Id);
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
                step.Artifacts = await db.GetAllStepArtifacts(step.Id);
                step.Commands = await db.GetAllCommands(step.Id);
            });

            return steps;
        }
        
        internal static async Task UpdateStepState(this IDbConnection db, ulong id, StepState state)
        {
            await db.UpdateOnlyAsync(() => new Step { State = state }, where: p => p.Id == id);
        }
        
        internal static async Task CreateOrUpdateStepVariables(this IDbConnection db, IEnumerable<StepVariable> variables)
        {
            if (variables == null) return;

            await variables.Iter(db.CreateOrUpdateStepVariable);
        }

        internal static async Task<ulong> CreateOrUpdateStepVariable(this IDbConnection db, StepVariable variable)
        {
            await db.SaveAsync(variable, true);
            
            return variable.Id;
        }

        internal static async Task<StepVariable> GetStepVariable(this IDbConnection db, ulong id)
        {
            return await db.LoadSingleByIdAsync<StepVariable>(id);
        }

        internal static async Task<ulong> GetStepVariablePkByVariableId(this IDbConnection db, ulong stepId, string variableId)
        {
            var q = db
                .From<StepVariable>()
                .Where(c => c.VariableId == variableId && c.StepId == stepId)
                .Select(c => c.Id);

            return await db.ScalarAsync<ulong>(q);
        }
        
        internal static async Task<List<StepVariable>> GetAllStepVariables(this IDbConnection db, ulong stepId)
        {
            return await db.LoadSelectAsync<StepVariable>(c => c.StepId == stepId);
        }

        internal static async Task CreateOrUpdateStepOptions(this IDbConnection db, IEnumerable<StepOption> options)
        {
            if (options == null) return;

            await options.Iter(db.CreateOrUpdateStepOption);
        }

        internal static async Task<ulong> CreateOrUpdateStepOption(this IDbConnection db, StepOption option)
        {
            await db.SaveAsync(option, true);
            return option.Id;
        }

        internal static async Task<StepOption> GetStepOption(this IDbConnection db, ulong id)
        {
            return await db.LoadSingleByIdAsync<StepOption>(id);
        }

        internal static async Task<List<StepOption>> GetAllStepOptions(this IDbConnection db, ulong stepId)
        {
            return await db.LoadSelectAsync<StepOption>(c => c.StepId == stepId);
        }

        internal static async Task<ulong> GetStepOptionPkByOptionId(this IDbConnection db, ulong stepId, string optionId)
        {
            var q = db
                .From<StepOption>()
                .Where(c => c.OptionId == optionId && c.StepId == stepId)
                .Select(c => c.Id);

            return await db.ScalarAsync<ulong>(q);
        }

        internal static async Task CreateOrUpdateStepArtifacts(this IDbConnection db, IEnumerable<StepArtifact> artifacts)
        {
            if (artifacts == null) return;

            await artifacts.Iter(db.CreateOrUpdateStepArtifact);
        }
        
        internal static async Task<ulong> CreateOrUpdateStepArtifact(this IDbConnection db, StepArtifact artifact)
        {
            await db.SaveAsync(artifact, true);
            
            return artifact.Id;
        }
        
        internal static async Task UpdateStepArtifact(this IDbConnection db, StepArtifact artifact)
        {
            await db.UpdateAsync(artifact);
        }
        
        internal static async Task<ulong> GetStepArtifactPkByArtifactId(this IDbConnection db, ulong stepId, string artifactId)
        {
            var q = db
                .From<StepArtifact>()
                .Where(c => c.ArtifactId == artifactId && c.StepId == stepId)
                .Select(c => c.Id);

            return await db.ScalarAsync<ulong>(q);
        }

        internal static async Task<StepArtifact> GetStepArtifact(this IDbConnection db, ulong id)
        {
            return await db.LoadSingleByIdAsync<StepArtifact>(id);
        }
        
        internal static async Task<List<StepArtifact>> GetAllStepArtifacts(this IDbConnection db, ulong stepId)
        {
            return await db.LoadSelectAsync<StepArtifact>(a => a.StepId == stepId);
        }

        internal static async Task<ulong> CreateOrUpdateStepArtifactOption(this IDbConnection db, StepArtifactOption option)
        {
            await db.SaveAsync(option, true);
            
            return option.Id;
        }
        
        internal static async Task<StepArtifactOption> GetStepArtifactOption(this IDbConnection db, ulong id)
        {
            return await db.LoadSingleByIdAsync<StepArtifactOption>(id);
        }
 
        internal static async Task<List<StepArtifactOption>> GetAllStepArtifactOptions(this IDbConnection db, ulong stepArtifactId)
        {
            return await db.LoadSelectAsync<StepArtifactOption>(c => c.StepArtifactId == stepArtifactId);
        }

        internal static async Task<ulong> GetStepArtifactOptionPkByOptionId(this IDbConnection db, ulong stepArtifactId, string optionId)
        {
            var q = db
                .From<StepArtifactOption>()
                .Where(c => c.OptionId == optionId && c.StepArtifactId == stepArtifactId)
                .Select(c => c.Id);

            return await db.ScalarAsync<ulong>(q);
        }
    }
}