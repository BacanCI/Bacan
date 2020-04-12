using CommandLine;

namespace Bakana.Options
{
    [Verb("producer", Hidden = true, HelpText = "Performs operations on the Producer")]
    public class ProducerOptions
    {
        [Value(0, MetaName = "Operation",
            HelpText = "Operation to perform on the producer",
            Required = true)]
        public ProducerOperation? Operation { get; set; }
    }
}