using System.Collections.Generic;
using System.Threading.Tasks;
using Bakana.Core.Entities;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Bakana.Core.Repositories
{
    public class StepRepository : IStepRepository
    {
        private readonly IDbConnectionFactory dbConnectionFactory;

        public StepRepository(IDbConnectionFactory dbConnectionFactory)
        {
            this.dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<ulong> CreateOrUpdate(Step step)
        {
            using (var db = await dbConnectionFactory.OpenAsync())
            {
                using (var tx = db.OpenTransaction())
                {
                    await db.CreateOrUpdateStep(step);

                    tx.Commit();

                    return step.Id;
                }
            }
        }

        public async Task Delete(ulong stepId)
        {
            using (var db = await dbConnectionFactory.OpenAsync())
            {
                await db.DeleteAsync(new Step { Id = stepId});
            }
        }

        public async Task<Step> Get(ulong stepId)
        {
            using (var db = await dbConnectionFactory.OpenAsync())
            {
                return await db.GetStep(stepId);
            }
        }
        
        public async Task<List<Step>> GetAll(string batchId)
        {
            using (var db = await dbConnectionFactory.OpenAsync())
            {
                return await db.GetAllSteps(batchId);
            }
        }

        public async Task CreateOrUpdateStepArtifact(StepArtifact artifact)
        {
            using (var db = await dbConnectionFactory.OpenAsync())
            {
                await db.CreateOrUpdateStepArtifact(artifact);
            }
        }

        public async Task CreateOrUpdateStepVariable(StepVariable variable)
        {
            using (var db = await dbConnectionFactory.OpenAsync())
            {
                await db.CreateOrUpdateStepVariable(variable);
            }
        }
        
        public async Task CreateOrUpdateStepOption(StepOption option)
        {
            using (var db = await dbConnectionFactory.OpenAsync())
            {
                await db.CreateOrUpdateStepOption(option);
            }
        }
    }
}