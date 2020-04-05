using ServiceStack;

namespace Bakana.ServiceModels
{
    public class StepArtifact : Artifact
    {
        [ApiMember(
            Description = "Set to true when artifact is created by Step Worker")]
        public bool OutputArtifact { get; set; }
    }
}