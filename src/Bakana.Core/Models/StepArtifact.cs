using ServiceStack.DataAnnotations;

namespace Bakana.Core.Models
{
    [UniqueConstraint(nameof(StepId), nameof(FileName))]
    public class StepArtifact : Artifact
    {
        [References(typeof(Step))]
        public string StepId { get; set; }
    }
}