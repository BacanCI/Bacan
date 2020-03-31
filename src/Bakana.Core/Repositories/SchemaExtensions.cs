using System.Data;
using System.Threading.Tasks;
using Bakana.Core.Entities;
using ServiceStack.OrmLite;

namespace Bakana.Core.Repositories
{
    public static class SchemaExtensions
    {
        public static void CreateBakanaTables(this IDbConnection db)
        {
            db.CreateTable<Batch>();
            db.CreateTable<CommandOption>();
            db.CreateTable<CommandVariable>();
            db.CreateTable<Command>();
                        
            db.CreateTable<StepArtifactOption>();
            db.CreateTable<StepArtifact>();
            db.CreateTable<StepVariable>();
            db.CreateTable<StepOption>();
            db.CreateTable<Step>();
                        
            db.CreateTable<BatchArtifactOption>();
            db.CreateTable<BatchArtifact>();
            db.CreateTable<BatchVariable>();
            db.CreateTable<BatchOption>();
            db.CreateTable<Batch>();
        }

        public static void DropBakanaTables(this IDbConnection db)
        {
            db.DropTable<CommandOption>();
            db.DropTable<CommandVariable>();
            db.DropTable<Command>();
                        
            db.DropTable<StepArtifactOption>();
            db.DropTable<StepArtifact>();
            db.DropTable<StepVariable>();
            db.DropTable<StepOption>();
            db.DropTable<Step>();
                        
            db.DropTable<BatchArtifactOption>();
            db.DropTable<BatchArtifact>();
            db.DropTable<BatchVariable>();
            db.DropTable<BatchOption>();
            db.DropTable<Batch>();
        }
    }
}