using Bakana.Core;
using Bakana.Core.Models;
using Bakana.ServiceModels;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.Text;
using Command = Bakana.Core.Models.Command;

namespace Bakana.ServiceInterface
{
    public class BatchServices : Service
    {
        private readonly IShortIdGenerator idGenerator;
        private readonly IDbConnectionFactory dbConnectionFactory;

        public BatchServices(
            IShortIdGenerator idGenerator, 
            IDbConnectionFactory dbConnectionFactory)
        {
            this.idGenerator = idGenerator;
            this.dbConnectionFactory = dbConnectionFactory;
        }
        
        public CreateBatchResponse Post(CreateBatchRequest request)
        {
            var batch = request.ConvertTo<Batch>();
            batch.Id = idGenerator.Generate();
            
            using (var db = dbConnectionFactory.Open())
            {
                db.CreateTable<CommandOption>();
                db.CreateTable<CommandVariable>();
                db.CreateTable<Command>();
                
                db.CreateTable<ArtifactOption>();
                db.CreateTable<StepArtifact>();
                db.CreateTable<StepVariable>();
                db.CreateTable<StepOption>();
                db.CreateTable<Step>();
                
                db.CreateTable<BatchVariable>();
                db.CreateTable<BatchOption>();
                db.CreateTable<BatchArtifact>();
                db.CreateTable<Batch>();

                db.Insert(batch);
                
                var result = db.SingleById<Batch>(batch.Id);
                result.PrintDump(); //= {Id: 1, Name:Seed Data}
            }
            return new CreateBatchResponse
            {
                Id = batch.Id
            };
        }
    }
}
