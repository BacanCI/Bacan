using Bakana.Core.Repositories;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Bakana
{
    public class ConfigureDb : IConfigureServices
    {
        public void Configure(IServiceCollection services)
        {
            var dbConnectionFactory = new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider);
            //var dbConnectionFactory = new OrmLiteConnectionFactory("Data Source=c:/temp/bakana.sqlite;Read Only=false", SqliteDialect.Provider);
            using (var db = dbConnectionFactory.Open())
            {
                db.DropBakanaTables();
                db.CreateBakanaTables();
            }
            
            OrmLiteUtils.PrintSql();
            
            services.AddSingleton<IDbConnectionFactory>(dbConnectionFactory);
        }
    }
}
