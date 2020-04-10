using ServiceStack.DataAnnotations;

namespace Bakana.Core.Entities
{
    [UniqueConstraint(nameof(CommandId), nameof(Name))]
    public class CommandOption : Option
    {
        [ForeignKey(typeof(Command), OnDelete = "CASCADE", OnUpdate = "CASCADE")]
        public ulong CommandId { get; set; }
    }
}