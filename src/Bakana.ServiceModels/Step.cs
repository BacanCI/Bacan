using System.Collections.Generic;

namespace Bakana.ServiceModels
{
    public class Step
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string[] Dependencies { get; set; }

        public string[] Tags { get; set; }

        public string[] Requirements { get; set; }

        public List<Option> Options { get; set; }

        public List<Variable> Variables { get; set; }

        public List<StepArtifact> Artifacts { get; set; }

        public List<Command> Commands { get; set; }
    }
}