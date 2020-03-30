using System.Threading.Tasks;
using Bakana.Core.Entities;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Bakana.Core.Repositories
{
    public class BatchRepository : IBatchRepository
    {
        // db.CreateTable<CommandOption>();
        // db.CreateTable<CommandVariable>();
        // db.CreateTable<Command>();
        //         
        // db.CreateTable<ArtifactOption>();
        // db.CreateTable<StepArtifact>();
        // db.CreateTable<StepVariable>();
        // db.CreateTable<StepOption>();
        // db.CreateTable<Step>();
        //         
        // db.CreateTable<BatchVariable>();
        // db.CreateTable<BatchOption>();
        // db.CreateTable<BatchArtifact>();
        // db.CreateTable<Batch>();

        private readonly IDbConnectionFactory dbConnectionFactory;

        public BatchRepository(IDbConnectionFactory dbConnectionFactory)
        {
            this.dbConnectionFactory = dbConnectionFactory;
        }
        
        public async Task Create(Batch batch)
        {
            using (var db = await dbConnectionFactory.OpenAsync())
            {
                await db.InsertAsync(batch);
            }
        }

        public async Task Update(Batch batch)
        {
            using (var db = await dbConnectionFactory.OpenAsync())
            {
                await db.UpdateAsync(batch);
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
                return await db.SingleAsync<Batch>(b => b.Id == batchId);
            }
        }
    }
}