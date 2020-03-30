using ServiceStack.DataAnnotations;

namespace Bakana.Core.Entities
{
    [UniqueConstraint(nameof(BatchId), nameof(FileName))]
    public class BatchArtifact : Artifact
    {
        [References(typeof(Step))]
        public string BatchId { get; set; }
    }
}