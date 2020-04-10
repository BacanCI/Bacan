using System.Collections.Generic;
using ServiceStack.DataAnnotations;

namespace Bakana.Core.Entities
{
    [UniqueConstraint(nameof(StepId), nameof(Name))]
    public class Command
    {
        [AutoIncrement]
        public ulong Id { get; set; }

        [ForeignKey(typeof(Step), OnDelete = "CASCADE", OnUpdate = "CASCADE")]
        public ulong StepId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Item { get; set; }

        public CommandState State { get; set; }

        [Reference]
        public List<CommandOption> Options { get; set; }
        
        [Reference]
        public List<CommandVariable> Variables { get; set; }
    }
}