using ServiceStack.DataAnnotations;

namespace Bakana.Core.Entities
{
    [UniqueConstraint(nameof(StepArtifactId), nameof(Name))]
    public class StepArtifactOption : Option
    {
        [References(typeof(StepArtifact))]
        public ulong StepArtifactId { get; set; }
    }
}