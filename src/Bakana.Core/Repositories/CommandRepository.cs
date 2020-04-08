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

        public async Task<bool> Update(Command command)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                var rowsUpdated = await db.UpdateCommand(command);
                return rowsUpdated > 0;
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
        
        public async Task<Command> Get(ulong stepId, string commandId)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                var id = await db.GetCommandPkByCommandId(stepId, commandId);
                return await db.GetCommand(id);
            }
        }

        public async Task<List<Command>> GetAll(ulong stepId)
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

        public async Task<bool> DoesCommandExist(string batchId, string stepId, string commandId)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                return await db.DoesCommandExist(batchId, stepId, commandId);
            }
        }

        public async Task<ulong> CreateOrUpdateCommandVariable(CommandVariable variable)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                return await db.CreateOrUpdateCommandVariable(variable);
            }
        }

        public async Task<CommandVariable> GetCommandVariable(ulong id)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                return await db.GetCommandVariable(id);
            }
        }

        public async Task<CommandVariable> GetCommandVariable(ulong commandId, string variableId)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                var id = await db.GetCommandVariablePkByVariableId(commandId, variableId);
                return await db.GetCommandVariable(id);
            }
        }

        public async Task<List<CommandVariable>> GetAllCommandVariables(ulong commandId)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                return await db.GetAllCommandVariables(commandId);
            }
        }

        public async Task DeleteCommandVariable(ulong id)
        {
            await DeleteByIdAsync<CommandVariable>(id);
        }

        public async Task<bool> DoesCommandVariableExist(string batchId, string stepId, string commandId, string variableId)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                return await db.DoesCommandVariableExist(batchId, stepId, commandId, variableId);
            }
        }

        public async Task<ulong> CreateOrUpdateCommandOption(CommandOption option)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                return await db.CreateOrUpdateCommandOption(option);
            }
        }

        public async Task<CommandOption> GetCommandOption(ulong id)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                return await db.GetCommandOption(id);
            }
        }

        public async Task<CommandOption> GetCommandOption(ulong commandId, string optionId)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                var id = await db.GetCommandOptionPkByOptionId(commandId, optionId);
                return await db.GetCommandOption(id);
            }
        }

        public async Task<List<CommandOption>> GetAllCommandOptions(ulong commandId)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                return await db.GetAllCommandOptions(commandId);
            }
        }

        public async Task DeleteCommandOption(ulong id)
        {
            await DeleteByIdAsync<CommandOption>(id);
        }

        public async Task<bool> DoesCommandOptionExist(string batchId, string stepId, string commandId, string optionId)
        {
            using (var db = await DbConnectionFactory.OpenAsync())
            {
                return await db.DoesCommandOptionExist(batchId, stepId, commandId, optionId);
            }
        }
    }
}