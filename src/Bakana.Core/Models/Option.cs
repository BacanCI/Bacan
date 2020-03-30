using ServiceStack.DataAnnotations;

namespace Bakana.Core.Models
{
    public class Option
    {
        [AutoIncrement]
        public ulong Id { get; set; }
        
        public string Name { get; set; }

        public string Description { get; set; }

        public string Value { get; set; }
    }
}