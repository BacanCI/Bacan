using ServiceStack.DataAnnotations;

namespace Bakana.Core.Entities
{
    [UniqueConstraint(nameof(ArtifactId), nameof(Name))]
    public class ArtifactOption : Option
    {
        [References(typeof(Step))]
        public string ArtifactId { get; set; }
    }
}