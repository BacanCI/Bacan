using CommandLine;

namespace Bakana.Options
{
    [Verb("worker", HelpText = "Performs operations on the Worker")]
    public class WorkerOptions
    {
        [Value(0, MetaName = "Operation",
            HelpText = "Operation to perform on the worker",
            Required = true)]
        public WorkerOperation? Operation { get; set; }
    }
}