using System.Collections.Generic;
using ServiceStack.DataAnnotations;

namespace Bakana.Core.Entities
{
    public class Artifact
    {
        [AutoIncrement]
        public ulong Id { get; set; }
        
        public string Description { get; set; }

        public string FileName { get; set; }
        
        [Reference]
        public List<ArtifactOption> Options { get; set; }
    }
}