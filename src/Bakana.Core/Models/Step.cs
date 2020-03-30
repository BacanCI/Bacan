using System.Collections.Generic;
using ServiceStack.DataAnnotations;

namespace Bakana.Core.Models
{
    public class Step
    {
        [PrimaryKey]
        public string Id { get; set; }
        
        [References(typeof(Batch))]
        public string BatchId { get; set; }
        
        public string Description { get; set; }
        
        public string[] Dependencies { get; set; }

        public string[] Tags { get; set; }

        public string[] Requirements { get; set; }

        [Reference]
        public List<StepOption> Options { get; set; }
        
        [Reference]
        public List<StepVariable> Variables { get; set; }
        
        [Reference]
        public List<StepArtifact> InputArtifacts { get; set; }
        
        [Reference]
        public List<StepArtifact> OutputArtifacts { get; set; }
        
        [Reference]
        public List<Command> Commands { get; set; }
    }
}