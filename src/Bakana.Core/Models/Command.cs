using System.Collections.Generic;
using ServiceStack.DataAnnotations;

namespace Bakana.Core.Models
{
    public class Command
    {
        [PrimaryKey]
        public string Id { get; set; }

        public string Description { get; set; }

        public string Item { get; set; }

        [Reference]
        public List<CommandOption> Options { get; set; }
        
        [Reference]
        public List<CommandVariable> Variables { get; set; }
    }
}