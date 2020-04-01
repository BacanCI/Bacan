using System.Threading.Tasks;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Bakana.Core.Repositories
{
    public abstract class RepositoryBase
    {
        protected readonly IDbConnectionFactory DbConnectionFactory;

        protected RepositoryBase(IDbConnectionFactory dbConnectionFactory)
        {
            DbConnectionFactory = dbConnectionFactory;
        }

        protected async Task DeleteByIdAsync<T>(ulong id)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                await db.DeleteByIdAsync<T>(id);
            }
        }
    }
}
