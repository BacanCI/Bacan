namespace Bakana.UnitTests.Operations.Arguments
{
    public class ArgumentOptionsData<T>
    {
        public string CliArguments { get; set; }
        public T ExpectedOptions { get; set; }
    }
}