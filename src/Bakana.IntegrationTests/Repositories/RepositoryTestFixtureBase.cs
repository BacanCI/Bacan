using System.Threading.Tasks;
using Bakana.Core.Repositories;
using NUnit.Framework;
using ServiceStack.OrmLite;

namespace Bakana.IntegrationTests.Repositories
{
    public abstract class RepositoryTestFixtureBase
    {
        protected OrmLiteConnectionFactory DbConnectionFactory;
        
        [SetUp]
        public virtual async Task Setup()
        {
            DbConnectionFactory = new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider);

            using var db = await DbConnectionFactory.OpenDbConnectionAsync();
            db.CreateBakanaTables();
        }

        [TearDown]
        public async Task TearDown()
        {
            using var db = await DbConnectionFactory.OpenDbConnectionAsync();
            db.DropBakanaTables();
        }
    }
}
