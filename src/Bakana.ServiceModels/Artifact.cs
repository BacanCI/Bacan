using System.Collections.Generic;
using ServiceStack;

namespace Bakana.ServiceModels
{
    public class Artifact
    {
        [ApiMember(
            Description = "A user-generated identifier associated with the artifact")]
        public string Name { get; set; }
        
        [ApiMember(
            Description = "A description of the artifact")]
        public string Description { get; set; }

        [ApiMember(
            Description = "The artifact's filename")]
        public string FileName { get; set; }

        [ApiMember(
            Description = "An array of options associated with the artifact")]
        public List<Option> Options { get; set; }
    }
}