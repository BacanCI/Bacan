using System.Collections.Generic;
using ServiceStack.DataAnnotations;

namespace Bakana.Core.Entities
{
    [UniqueConstraint(nameof(BatchId), nameof(FileName))]
    public class BatchArtifact
    {
        [AutoIncrement]
        public ulong Id { get; set; }
        
        [ForeignKey(typeof(Batch), OnDelete = "CASCADE", OnUpdate = "CASCADE")]
        public string BatchId { get; set; }

        public string Description { get; set; }

        public string FileName { get; set; }
        
        [Reference]
        public List<BatchArtifactOption> Options { get; set; }
    }
}