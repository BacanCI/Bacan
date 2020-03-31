using System.Collections.Generic;
using System.Threading.Tasks;
using Bakana.Core.Entities;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Bakana.Core.Repositories
{
    public class CommandRepository : ICommandRepository
    {
        private readonly IDbConnectionFactory dbConnectionFactory;

        public CommandRepository(IDbConnectionFactory dbConnectionFactory)
        {
            this.dbConnectionFactory = dbConnectionFactory;
        }

        public async Task CreateOrUpdate(Command command)
        {
            using (var db = await dbConnectionFactory.OpenAsync())
            {
                using (var tx = db.OpenTransaction())
                {
                    await db.CreateOrUpdateCommand(command);

                    tx.Commit();
                }
            }
        }

        public async Task Delete(string commandId)
        {
            using (var db = await dbConnectionFactory.OpenAsync())
            {
                await db.DeleteAsync(new Command { Id = commandId});
            }
        }

        public async Task<Command> Get(string commandId)
        {
            using (var db = await dbConnectionFactory.OpenAsync())
            {
                return await db.LoadSingleByIdAsync<Command>(commandId);
            }
        }
        
        public async Task<IList<Command>> GetAll(string stepId)
        {
            using (var db = await dbConnectionFactory.OpenAsync())
            {
                return await db.GetAllCommands(stepId);
            }
        }

        public async Task CreateOrUpdateCommandVariable(CommandVariable variable)
        {
            using (var db = await dbConnectionFactory.OpenAsync())
            {
                await db.CreateOrUpdateCommandVariable(variable);
            }
        }
        
        public async Task CreateOrUpdateCommandOption(CommandOption option)
        {
            using (var db = await dbConnectionFactory.OpenAsync())
            {
                await db.CreateOrUpdateCommandOption(option);
            }
        }
    }
}