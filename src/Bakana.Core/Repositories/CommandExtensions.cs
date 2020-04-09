using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Bakana.Core.Entities;
using Bakana.Core.Extensions;
using ServiceStack.OrmLite;

namespace Bakana.Core.Repositories
{
    public static class CommandExtensions
    {
        internal static async Task CreateCommands(this IDbConnection db, IEnumerable<Command> commands)
        {
            await commands.Iter(db.CreateCommand);
        }

        internal static async Task<ulong> CreateCommand(this IDbConnection db, Command command)
        {
            await db.SaveAsync(command, true);

            await db.CreateOrUpdateCommandOptions(command.Options);
            await db.CreateOrUpdateCommandVariables(command.Variables);

            return command.Id;
        }

        internal static async Task<int> UpdateCommand(this IDbConnection db, Command command)
        {
            return await db.UpdateAsync(command);
        }
        
        internal static async Task<Command> GetCommand(this IDbConnection db, ulong id)
        {
            return await db.LoadSingleByIdAsync<Command>(id);
        }

        internal static async Task<List<Command>> GetAllCommands(this IDbConnection db, ulong stepId)
        {
            return await db.LoadSelectAsync<Command>(c => c.StepId == stepId);
        }
        
        internal static async Task UpdateCommandState(this IDbConnection db, ulong id, CommandState state)
        {
            await db.UpdateOnlyAsync(() => new Command { State = state }, where: p => p.Id == id);
        }

        internal static async Task<ulong> GetCommandPkByCommandId(this IDbConnection db, ulong stepId, string commandId)
        {
            var q = db
                .From<Command>()
                .Where(c => c.CommandId == commandId && c.StepId == stepId)
                .Select(c => c.Id);

            return await db.ScalarAsync<ulong>(q);
        }

        internal static async Task<bool> DoesCommandExist(this IDbConnection db, string batchId, string stepId, string commandId)
        {
            var id = await db.GetStepPkByStepId(batchId, stepId);
            
            return await db.ExistsAsync<Command>(o => o.StepId == id && o.CommandId == commandId);
        }

        internal static async Task CreateOrUpdateCommandOptions(this IDbConnection db, IEnumerable<CommandOption> options)
        {
            await options.Iter(db.CreateOrUpdateCommandOption);
        }

        internal static async Task<ulong> CreateOrUpdateCommandOption(this IDbConnection db, CommandOption option)
        {
            await db.SaveAsync(option, true);
            
            return option.Id;
        }

        internal static async Task<CommandOption> GetCommandOption(this IDbConnection db, ulong id)
        {
            return await db.LoadSingleByIdAsync<CommandOption>(id);
        }

        internal static async Task<List<CommandOption>> GetAllCommandOptions(this IDbConnection db, ulong commandId)
        {
            return await db.LoadSelectAsync<CommandOption>(c => c.CommandId == commandId);
        }

        internal static async Task<ulong> GetCommandOptionPkByOptionId(this IDbConnection db, ulong commandId, string optionId)
        {
            var q = db
                .From<CommandOption>()
                .Where(c => c.OptionId == optionId && c.CommandId == commandId)
                .Select(c => c.Id);

            return await db.ScalarAsync<ulong>(q);
        }

        internal static async Task<bool> DoesCommandOptionExist(this IDbConnection db, string batchId, string stepId, string commandId, string optionId)
        {
            var id = await db.GetStepPkByStepId(batchId, stepId);
            var cId = await db.GetCommandPkByCommandId(id, commandId);
            
            return await db.ExistsAsync<CommandOption>(o => o.CommandId == cId && o.OptionId == optionId);
        }

        internal static async Task CreateOrUpdateCommandVariables(this IDbConnection db, IEnumerable<CommandVariable> variables)
        {
            await variables.Iter(db.CreateOrUpdateCommandVariable);
        }

        internal static async Task<ulong> CreateOrUpdateCommandVariable(this IDbConnection db, CommandVariable variable)
        {
            await db.SaveAsync(variable, true);
            
            return variable.Id;
        }

        internal static async Task<CommandVariable> GetCommandVariable(this IDbConnection db, ulong id)
        {
            return await db.LoadSingleByIdAsync<CommandVariable>(id);
        }

        internal static async Task<List<CommandVariable>> GetAllCommandVariables(this IDbConnection db, ulong commandId)
        {
            return await db.LoadSelectAsync<CommandVariable>(c => c.CommandId == commandId);
        }

        internal static async Task<ulong> GetCommandVariablePkByVariableId(this IDbConnection db, ulong commandId, string variableId)
        {
            var q = db
                .From<CommandVariable>()
                .Where(c => c.VariableId == variableId && c.CommandId == commandId)
                .Select(c => c.Id);

            return await db.ScalarAsync<ulong>(q);
        }
        
        internal static async Task<bool> DoesCommandVariableExist(this IDbConnection db, string batchId, string stepId, string commandId, string variableId)
        {
            var id = await db.GetStepPkByStepId(batchId, stepId);
            var cId = await db.GetCommandPkByCommandId(id, commandId);
            
            return await db.ExistsAsync<CommandVariable>(o => o.CommandId == cId && o.VariableId == variableId);
        }
    }
}