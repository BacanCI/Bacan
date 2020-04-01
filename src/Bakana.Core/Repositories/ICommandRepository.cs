using System.Collections.Generic;
using System.Threading.Tasks;
using Bakana.Core.Entities;

namespace Bakana.Core.Repositories
{
    public interface ICommandRepository
    {
        Task<ulong> Create(Command command);
        Task Update(Command command);
        Task UpdateByCommandId(Command command);
        Task Delete(ulong id);
        Task<Command> Get(ulong id);
        Task<IList<Command>> GetAll(ulong stepId);
        Task UpdateState(ulong id, CommandState state);

        Task CreateOrUpdateCommandVariable(CommandVariable variable);
        Task DeleteCommandVariable(ulong id);
        Task CreateOrUpdateCommandOption(CommandOption option);
        Task DeleteCommandOption(ulong id);
        Task Delete(string commandId);
        Task<Command> Get(string commandId);
        Task<IList<Command>> GetAll(string stepId);
        Task UpdateState(string commandId, CommandState state);
    }
}