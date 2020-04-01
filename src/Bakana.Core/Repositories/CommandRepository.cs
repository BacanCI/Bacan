using System.Collections.Generic;
using System.Threading.Tasks;
using Bakana.Core.Entities;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Bakana.Core.Repositories
{
    public class CommandRepository : RepositoryBase, ICommandRepository
    {
        public CommandRepository(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
        {
        }

        public async Task<ulong> Create(Command command)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                using (var tx = db.OpenTransaction())
                {
                    await db.CreateCommand(command);

                    tx.Commit();

                    return command.Id;
                }
            }
        }

        public async Task Update(Command command)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                await db.UpdateCommand(command);
            }
        }

        public async Task Delete(ulong id)
        {
            await DeleteByIdAsync<Command>(id);
        }

        public async Task<Command> Get(ulong id)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                return await db.GetCommand(id);
            }
        }
        
        public async Task<IList<Command>> GetAll(ulong stepId)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                return await db.GetAllCommands(stepId);
            }
        }

        public async Task UpdateState(ulong id, CommandState state)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                await db.UpdateCommandState(id, state);
            }
        }

        public async Task CreateOrUpdateCommandVariable(CommandVariable variable)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                await db.CreateOrUpdateCommandVariable(variable);
            }
        }

        public async Task DeleteCommandVariable(ulong id)
        {
            await DeleteByIdAsync<CommandVariable>(id);
        }

        public async Task CreateOrUpdateCommandOption(CommandOption option)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                await db.CreateOrUpdateCommandOption(option);
            }
        }

        public async Task DeleteCommandOption(ulong id)
        {
            await DeleteByIdAsync<CommandOption>(id);
        }
    }
}