using Bakana.Core.Entities;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.Text;

namespace Bakana
{
    public class ConfigureDb : IConfigureServices
    {
        public void Configure(IServiceCollection services)
        {
            //var dbConnectionFactory = new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider);
            var dbConnectionFactory = new OrmLiteConnectionFactory("Data Source=c:/temp/bakana.sqlite;Read Only=false", SqliteDialect.Provider);
            using (var db = dbConnectionFactory.Open())
            {
                db.DropTable<CommandOption>();
                db.DropTable<CommandVariable>();
                db.DropTable<Core.Entities.Command>();
                        
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

                db.CreateTable<Batch>();
                db.CreateTable<CommandOption>();
                db.CreateTable<CommandVariable>();
                db.CreateTable<Core.Entities.Command>();
                        
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
            
            OrmLiteUtils.PrintSql();
            
            services.AddSingleton<IDbConnectionFactory>(dbConnectionFactory);
        }
    }
}
