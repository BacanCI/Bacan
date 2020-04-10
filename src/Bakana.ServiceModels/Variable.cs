using ServiceStack;

namespace Bakana.ServiceModels
{
    public class Variable
    {
        [ApiMember(
            Description = "A user-generated identifier associated with the variable")]
        public string Name { get; set; }

        [ApiMember(
            Description = "A description of the variable")]
        public string Description { get; set; }

        [ApiMember(
            Description = "The value assigned to the variable")]
        public string Value { get; set; }

        [ApiMember(
            Description = "Set to true if the variable is considered sensitive or secure")]
        public bool Sensitive { get; set; }
    }
}