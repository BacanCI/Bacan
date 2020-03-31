using ServiceStack.DataAnnotations;

namespace Bakana.Core.Entities
{
    [UniqueConstraint(nameof(CommandId), nameof(Name))]
    public class CommandVariable : Variable
    {
        [References(typeof(Command))]
        public string CommandId { get; set; }
    }
}