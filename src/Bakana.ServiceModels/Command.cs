using System.Collections.Generic;
using ServiceStack.DataAnnotations;

namespace Bakana.ServiceModels
{
    public class Command
    {
        public string CommandId { get; set; }

        public string Description { get; set; }

        public string Item { get; set; }

        public List<Option> Options { get; set; }

        public List<Variable> Variables { get; set; }
    }
}