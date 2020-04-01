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
            if (commands == null) return;

            await commands.Iter(db.CreateCommand);
        }

        internal static async Task<ulong> CreateCommand(this IDbConnection db, Command command)
        {
            await db.SaveAsync(command, true);

            await db.CreateOrUpdateCommandOptions(command.Options);
            await db.CreateOrUpdateCommandVariables(command.Variables);

            return command.Id;
        }

        internal static async Task UpdateCommand(this IDbConnection db, Command command)
        {
            await db.UpdateAsync(command);
        }

        internal static async Task CreateOrUpdateCommandOptions(this IDbConnection db, IEnumerable<CommandOption> options)
        {
            if (options == null) return;

            await options.Iter(db.CreateOrUpdateCommandOption);
        }

        internal static async Task CreateOrUpdateCommandOption(this IDbConnection db, CommandOption option)
        {
            await db.SaveAsync(option, true);
        }
        
        internal static async Task CreateOrUpdateCommandVariables(this IDbConnection db, IEnumerable<CommandVariable> variables)
        {
            if (variables == null) return;

            await variables.Iter(db.CreateOrUpdateCommandVariable);
        }

        internal static async Task CreateOrUpdateCommandVariable(this IDbConnection db, CommandVariable variable)
        {
            await db.SaveAsync(variable, true);
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
    }
}