using System.Threading.Tasks;
using Bakana.Core.Entities;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Bakana.Core.Repositories
{
    public class BatchRepository : IBatchRepository
    {
        private readonly IDbConnectionFactory dbConnectionFactory;

        public BatchRepository(IDbConnectionFactory dbConnectionFactory)
        {
            this.dbConnectionFactory = dbConnectionFactory;
        }
        
        public async Task Create(Batch batch)
        {
            using (var db = await dbConnectionFactory.OpenAsync())
            {
                using (var tx = db.OpenTransaction())
                {
                    await db.CreateBatch(batch);

                    tx.Commit();
                }
            }
        }

        public async Task Update(Batch batch)
        {
            using (var db = await dbConnectionFactory.OpenAsync())
            {
                await db.UpdateBatch(batch);
            }
        }

        public async Task Delete(string batchId)
        {
            using (var db = await dbConnectionFactory.OpenAsync())
            {
                await db.DeleteAsync(new Batch { Id = batchId});
            }
        }

        public async Task<Batch> Get(string batchId)
        {
            using (var db = await dbConnectionFactory.OpenAsync())
            {
                return await db.GetBatch(batchId);
            }
        }
        
        public async Task CreateOrUpdateBatchArtifact(BatchArtifact artifact)
        {
            using (var db = await dbConnectionFactory.OpenAsync())
            {
                await db.CreateOrUpdateBatchArtifact(artifact);
            }
        }

        public async Task CreateOrUpdateBatchVariable(BatchVariable variable)
        {
            using (var db = await dbConnectionFactory.OpenAsync())
            {
                await db.CreateOrUpdateBatchVariable(variable);
            }
        }
        
        public async Task CreateOrUpdateBatchOption(BatchOption option)
        {
            using (var db = await dbConnectionFactory.OpenAsync())
            {
                await db.CreateOrUpdateBatchOption(option);
            }
        }
    }
}