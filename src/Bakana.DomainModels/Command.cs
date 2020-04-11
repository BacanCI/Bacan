using System.Collections.Generic;

namespace Bakana.DomainModels
{
    public class Command
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Run { get; set; }

        public List<Option> Options { get; set; }

        public List<Variable> Variables { get; set; }
    }
}