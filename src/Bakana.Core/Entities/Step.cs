using System.Collections.Generic;
using ServiceStack.DataAnnotations;

namespace Bakana.Core.Entities
{
    [UniqueConstraint(nameof(BatchId), nameof(Name))]
    public class Step
    {
        [AutoIncrement]
        public ulong Id { get; set; }

        [ForeignKey(typeof(Batch), OnDelete = "CASCADE", OnUpdate = "CASCADE")]
        public string BatchId { get; set; }

        public string Name { get; set; }
       
        public string Description { get; set; }
        
        public string[] Dependencies { get; set; }

        public string[] Tags { get; set; }

        public string[] Requirements { get; set; }

        public StepState State { get; set; }

        [Reference]
        public List<StepOption> Options { get; set; }
        
        [Reference]
        public List<StepVariable> Variables { get; set; }
        
        [Reference]
        public List<StepArtifact> Artifacts { get; set; }
        
        [Reference]
        public List<Command> Commands { get; set; }
    }
}