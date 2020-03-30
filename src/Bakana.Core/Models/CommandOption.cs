using ServiceStack.DataAnnotations;

namespace Bakana.Core.Models
{
    [UniqueConstraint(nameof(CommandId), nameof(Name))]
    public class CommandOption : Option
    {
        [References(typeof(Command))]
        public string CommandId { get; set; }
    }
}