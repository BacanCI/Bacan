using System.Collections.Generic;

namespace Bakana.ServiceModels
{
    public class Artifact
    {
        public string ArtifactId { get; set; }
        
        public string Description { get; set; }

        public string FileName { get; set; }

        public List<Option> Options { get; set; }
    }
}