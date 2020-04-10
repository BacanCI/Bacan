using ServiceStack.DataAnnotations;

namespace Bakana.Core.Entities
{
    [UniqueConstraint(nameof(StepArtifactId), nameof(Name))]
    public class StepArtifactOption : Option
    {
        [ForeignKey(typeof(StepArtifact), OnDelete = "CASCADE", OnUpdate = "CASCADE")]
        public ulong StepArtifactId { get; set; }
    }
}