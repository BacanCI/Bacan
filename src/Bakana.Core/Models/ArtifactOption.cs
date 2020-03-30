using ServiceStack.DataAnnotations;

namespace Bakana.Core.Models
{
    [UniqueConstraint(nameof(ArtifactId), nameof(Name))]
    public class ArtifactOption : Option
    {
        [References(typeof(Step))]
        public string ArtifactId { get; set; }
    }
}