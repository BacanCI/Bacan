using System.Collections.Generic;

namespace Bakana.ServiceModels
{
    public class StepArtifact
    {
        public string Description { get; set; }

        public string FileName { get; set; }

        public bool OutputArtifact { get; set; }

        public List<Option> Options { get; set; }
    }
}