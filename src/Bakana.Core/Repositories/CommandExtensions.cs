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

        internal static async Task<List<Command>> GetAllCommands(this IDbConnection db, ulong stepName)
        {
            return await db.LoadSelectAsync<Command>(c => c.StepId == stepName);
        }
        
        internal static async Task UpdateCommandState(this IDbConnection db, ulong id, CommandState state)
        {
            await db.UpdateOnlyAsync(() => new Command { State = state }, where: p => p.Id == id);
        }

        internal static async Task<ulong> GetCommandPkByCommandName(this IDbConnection db, ulong stepName, string commandName)
        {
            var q = db
                .From<Command>()
                .Where(c => c.Name == commandName && c.StepId == stepName)
                .Select(c => c.Id);

            return await db.ScalarAsync<ulong>(q);
        }

        internal static async Task<bool> DoesCommandExist(this IDbConnection db, string batchId, string stepName, string commandName)
        {
            var id = await db.GetStepPkByStepName(batchId, stepName);
            
            return await db.ExistsAsync<Command>(o => o.StepId == id && o.Name == commandName);
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

        internal static async Task<ulong> GetCommandOptionPkByOptionName(this IDbConnection db, ulong commandId, string optionName)
        {
            var q = db
                .From<CommandOption>()
                .Where(c => c.Name == optionName && c.CommandId == commandId)
                .Select(c => c.Id);

            return await db.ScalarAsync<ulong>(q);
        }

        internal static async Task<bool> DoesCommandOptionExist(this IDbConnection db, string batchId, string stepName, string commandName, string optionName)
        {
            var id = await db.GetStepPkByStepName(batchId, stepName);
            var cId = await db.GetCommandPkByCommandName(id, commandName);
            
            return await db.ExistsAsync<CommandOption>(o => o.CommandId == cId && o.Name == optionName);
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

        internal static async Task<ulong> GetCommandVariablePkByVariableName(this IDbConnection db, ulong commandId, string variableName)
        {
            var q = db
                .From<CommandVariable>()
                .Where(c => c.Name == variableName && c.CommandId == commandId)
                .Select(c => c.Id);

            return await db.ScalarAsync<ulong>(q);
        }
        
        internal static async Task<bool> DoesCommandVariableExist(this IDbConnection db, string batchId, string stepName, string commandName, string variableName)
        {
            var id = await db.GetStepPkByStepName(batchId, stepName);
            var cId = await db.GetCommandPkByCommandName(id, commandName);
            
            return await db.ExistsAsync<CommandVariable>(o => o.CommandId == cId && o.Name == variableName);
        }
    }
}