namespace Bakana.ServiceModels
{
    public class Variable
    {
        public string VariableId { get; set; }

        public string Description { get; set; }

        public string Value { get; set; }

        public bool Sensitive { get; set; }
    }
}