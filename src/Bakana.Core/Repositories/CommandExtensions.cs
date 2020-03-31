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
        internal static async Task CreateOrUpdateCommands(this IDbConnection db, IEnumerable<Command> commands)
        {
            if (commands == null) return;

            await commands.Iter(db.CreateOrUpdateCommand);
        }

        internal static async Task CreateOrUpdateCommand(this IDbConnection db, Command command)
        {
            await db.SaveAsync(command, true);

            await db.CreateOrUpdateCommandOptions(command.Options);
            await db.CreateOrUpdateCommandVariables(command.Variables);
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
        
        internal static async Task<List<Command>> GetAllCommands(this IDbConnection db, string stepId)
        {
            return await db.LoadSelectAsync<Command>(c => c.StepId == stepId);
        }
    }
}