using ServiceStack;

namespace Bakana.ServiceModels
{
    public class Option
    {
        [ApiMember(
            Description = "A user-generated identifier associated with the option")]
        public string OptionId { get; set; }
        
        [ApiMember(
            Description = "A description of the option")]
        public string Description { get; set; }

        [ApiMember(
            Description = "The value assigned to the option")]
        public string Value { get; set; }
    }
}