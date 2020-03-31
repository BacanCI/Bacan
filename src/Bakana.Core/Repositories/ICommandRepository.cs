using System.Collections.Generic;
using System.Threading.Tasks;
using Bakana.Core.Entities;

namespace Bakana.Core.Repositories
{
    public interface ICommandRepository
    {
        Task CreateOrUpdate(Command command);
        Task Delete(string commandId);
        Task<Command> Get(string commandId);

        Task CreateOrUpdateCommandVariable(CommandVariable variable);
        Task CreateOrUpdateCommandOption(CommandOption option);
        Task<IList<Command>> GetAll(string stepId);
    }
}