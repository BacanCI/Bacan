using System.Collections.Generic;
using System.Threading.Tasks;
using Bakana.Core.Entities;

namespace Bakana.Core.Repositories
{
    public interface ICommandRepository
    {
        Task<ulong> Create(Command command);
        Task<bool> Update(Command command);
        Task Delete(ulong id);
        Task<Command> Get(ulong id);
        Task<Command> Get(ulong stepId, string commandName);
        Task<List<Command>> GetAll(ulong stepId);
        Task UpdateState(ulong id, CommandState state);
        Task<bool> DoesCommandExist(string batchId, string stepName, string commandName);
        
        Task<ulong> CreateOrUpdateCommandVariable(CommandVariable variable);
        Task<CommandVariable> GetCommandVariable(ulong id);
        Task<CommandVariable> GetCommandVariable(ulong commandId, string variableName);
        Task<List<CommandVariable>> GetAllCommandVariables(ulong commandId);
        Task DeleteCommandVariable(ulong id);
        Task<bool> DoesCommandVariableExist(string batchId, string stepName, string commandName, string variableName);
        
        Task<ulong> CreateOrUpdateCommandOption(CommandOption option);
        Task<CommandOption> GetCommandOption(ulong id);
        Task<CommandOption> GetCommandOption(ulong commandId, string optionName);
        Task<List<CommandOption>> GetAllCommandOptions(ulong commandId);
        Task DeleteCommandOption(ulong id);
        Task<bool> DoesCommandOptionExist(string batchId, string stepName, string commandName, string optionName);
    }
}