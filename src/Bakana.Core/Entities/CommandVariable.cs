using ServiceStack.DataAnnotations;

namespace Bakana.Core.Entities
{
    [UniqueConstraint(nameof(CommandId), nameof(Name))]
    public class CommandVariable : Variable
    {
        [References(typeof(Step))]
        public string CommandId { get; set; }
    }
}