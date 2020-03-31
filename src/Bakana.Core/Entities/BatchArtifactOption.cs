using ServiceStack.DataAnnotations;

namespace Bakana.Core.Entities
{
    [UniqueConstraint(nameof(BatchArtifactId), nameof(Name))]
    public class BatchArtifactOption : Option
    {
        [References(typeof(BatchArtifact))]
        public ulong BatchArtifactId { get; set; }
    }
}
