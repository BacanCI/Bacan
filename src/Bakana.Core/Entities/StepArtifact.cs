using System.Collections.Generic;
using ServiceStack.DataAnnotations;

namespace Bakana.Core.Entities
{
    [UniqueConstraint(nameof(StepId), nameof(FileName))]
    public class StepArtifact
    {
        [AutoIncrement]
        public ulong Id { get; set; }
        
        [ForeignKey(typeof(Step), OnDelete = "CASCADE", OnUpdate = "CASCADE")]
        public ulong StepId { get; set; }

        public string Description { get; set; }

        public string FileName { get; set; }
        
        public bool OutputArtifact { get; set; }
        
        [Reference]
        public List<StepArtifactOption> Options { get; set; }
    }
}