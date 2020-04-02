using ServiceStack.DataAnnotations;

namespace Bakana.Core.Entities
{
    [UniqueConstraint(nameof(CommandId), nameof(VariableId))]
    public class CommandVariable : Variable
    {
        [ForeignKey(typeof(Command), OnDelete = "CASCADE", OnUpdate = "CASCADE")]
        public ulong CommandId { get; set; }
    }
}