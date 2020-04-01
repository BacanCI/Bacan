using System.Collections.Generic;
using ServiceStack.DataAnnotations;

namespace Bakana.ServiceModels
{
    public class Command
    {
        public string CommandId { get; set; }

        public string Description { get; set; }

        public string Item { get; set; }

        [Reference]
        public List<Option> Options { get; set; }

        [Reference]
        public List<Variable> Variables { get; set; }
    }
}