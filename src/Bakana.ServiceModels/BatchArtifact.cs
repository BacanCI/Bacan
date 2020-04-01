using System.Collections.Generic;

namespace Bakana.ServiceModels
{
    public class BatchArtifact
    {
        public string Description { get; set; }

        public string FileName { get; set; }

        public List<Option> Options { get; set; }
    }
}