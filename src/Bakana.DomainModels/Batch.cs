using System.Collections.Generic;

namespace Bakana.DomainModels
{
    public class Batch
    {
        public string Description { get; set; }

        public List<Option> Options { get; set; }

        public List<Variable> Variables { get; set; }

        public List<BatchArtifact> Artifacts { get; set; }
        
        public List<Step> Steps { get; set; }
    }
}