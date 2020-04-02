using ServiceStack.DataAnnotations;

namespace Bakana.Core.Entities
{
    [UniqueConstraint(nameof(BatchArtifactId), nameof(OptionId))]
    public class BatchArtifactOption : Option
    {
        [ForeignKey(typeof(BatchArtifact), OnDelete = "CASCADE", OnUpdate = "CASCADE")]
        public ulong BatchArtifactId { get; set; }
    }
}
