using System.Collections.Generic;
using System.Threading.Tasks;
using Bakana.Core.Entities;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Bakana.Core.Repositories
{
    public class StepRepository : RepositoryBase, IStepRepository
    {
        public StepRepository(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
        {
        }

        public async Task<ulong> Create(Step step)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                using (var tx = db.OpenTransaction())
                {
                    await db.CreateStep(step);

                    tx.Commit();

                    return step.Id;
                }
            }
        }

        public async Task<bool> Update(Step step)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                var rowsUpdated = await db.UpdateStep(step);
                return rowsUpdated > 0;
            }
        }

        public async Task<bool> Delete(ulong stepId)
        {
            var rowsDeleted = await DeleteByIdAsync<Step>(stepId);
            return rowsDeleted > 0;
        }

        public async Task<Step> Get(ulong stepId)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                return await db.GetStep(stepId);
            }
        }

        public async Task<Step> Get(string batchId, string stepId)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                var id = await db.GetStepPkByStepId(batchId, stepId);
                return await db.GetStep(id);
            }
        }

        public async Task<List<Step>> GetAll(string batchId)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                return await db.GetAllSteps(batchId);
            }
        }

        public async Task UpdateState(ulong id, StepState state)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                await db.UpdateStepState(id, state);
            }
        }

        public async Task<bool> DoesStepExist(string batchId, string stepId)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                return await db.DoesStepExist(batchId, stepId);
            }
        }

        public async Task<ulong> CreateOrUpdateStepVariable(StepVariable variable)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                return await db.CreateOrUpdateStepVariable(variable);
            }
        }

        public async Task<StepVariable> GetStepVariable(ulong id)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                return await db.GetStepVariable(id);
            }
        }

        public async Task<StepVariable> GetStepVariable(ulong stepId, string variableId)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                var id = await db.GetStepVariablePkByVariableId(stepId, variableId);
                return await db.GetStepVariable(id);
            }
        }

        public async Task<List<StepVariable>> GetAllStepVariables(ulong stepId)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                return await db.GetAllStepVariables(stepId);
            }
        }

        public async Task DeleteStepVariable(ulong id)
        {
            await DeleteByIdAsync<StepVariable>(id);
        }

        public async Task<bool> DoesStepVariableExist(string batchId, string stepId, string variableId)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                return await db.DoesStepVariableExist(batchId, stepId, variableId);
            }
        }

        public async Task<ulong> CreateOrUpdateStepOption(StepOption option)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                return await db.CreateOrUpdateStepOption(option);
            }
        }

        public async Task<StepOption> GetStepOption(ulong id)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                return await db.GetStepOption(id);
            }
        }

        public async Task<StepOption> GetStepOption(ulong stepId, string optionId)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                var id = await db.GetStepOptionPkByOptionId(stepId, optionId);
                return await db.GetStepOption(id);
            }
        }

        public async Task<List<StepOption>> GetAllStepOptions(ulong stepId)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                return await db.GetAllStepOptions(stepId);
            }
        }

        public async Task DeleteStepOption(ulong id)
        {
            await DeleteByIdAsync<StepOption>(id);
        }

        public async Task<bool> DoesStepOptionExist(string batchId, string stepId, string optionId)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                return await db.DoesStepOptionExist(batchId, stepId, optionId);
            }
        }

        public async Task<ulong> CreateStepArtifact(StepArtifact stepArtifact)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                return await db.CreateOrUpdateStepArtifact(stepArtifact);
            }
        }

        public async Task UpdateStepArtifact(StepArtifact stepArtifact)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                await db.UpdateStepArtifact(stepArtifact);
            }
        }

        public async Task<StepArtifact> GetStepArtifact(ulong id)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                return await db.GetStepArtifact(id);
            }
        }

        public async Task<StepArtifact> GetStepArtifact(ulong stepId, string artifactId)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                var id = await db.GetStepArtifactPkByArtifactId(stepId, artifactId);
                return await db.GetStepArtifact(id);
            }
        }

        public async Task<List<StepArtifact>> GetAllStepArtifacts(ulong stepId)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                return await db.GetAllStepArtifacts(stepId);
            }
        }

        public async Task DeleteStepArtifact(ulong id)
        {
            await DeleteByIdAsync<StepArtifact>(id);
        }

        public async Task<bool> DoesStepArtifactExist(string batchId, string stepId, string artifactId)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                return await db.DoesStepArtifactExist(batchId, stepId, artifactId);
            }
        }

        public async Task<ulong> CreateOrUpdateStepArtifactOption(StepArtifactOption option)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                return await db.CreateOrUpdateStepArtifactOption(option);
            }
        }

        public async Task<StepArtifactOption> GetStepArtifactOption(ulong id)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                return await db.GetStepArtifactOption(id);
            }
        }

        public async Task<StepArtifactOption> GetStepArtifactOption(ulong artifactId, string optionId)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                var id = await db.GetStepArtifactOptionPkByOptionId(artifactId, optionId);
                return await db.GetStepArtifactOption(id);
            }
        }

        public async Task<List<StepArtifactOption>> GetAllStepArtifactOptions(ulong artifactId)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                return await db.GetAllStepArtifactOptions(artifactId);
            }
        }

        public async Task<bool> DeleteStepArtifactOption(ulong id)
        {
            var rowsDeleted = await DeleteByIdAsync<StepArtifactOption>(id);
            return rowsDeleted > 0;
        }

        public async Task<bool> DoesStepArtifactOptionExist(string batchId, string stepId, string artifactId, string optionId)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                return await db.DoesStepArtifactOptionExist(batchId, stepId, artifactId, optionId);
            }
        }
    }
}