using System.Collections.Generic;

namespace Bakana.DomainModels
{
    public class Artifact
    {
        public string Name { get; set; }
        
        public string Description { get; set; }

        public string FileName { get; set; }

        public List<Option> Options { get; set; }
    }
}