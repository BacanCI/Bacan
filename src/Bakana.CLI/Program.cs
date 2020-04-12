using System;
using System.Linq;
using System.Threading.Tasks;
using Bakana.Operations;
using Bakana.Options;
using CommandLine;
using CommandLine.Text;

namespace Bakana
{
    public class Program 
    {
        static async Task Main(string[] args)
        {
            args = new[]
            {
                "batch",
                "123",
                "Upload",
                "abc.zip",
                "--name",
                "def.zip"
            };

            var result = await Parser.Default
                .ParseArguments<InitOptions, InfoOptions, SetOptions, LoadOptions, BatchOptions, ProducerOptions, WorkerOptions>(args)
                .MapResult(
                    (InitOptions options) => new Init(options).Run(), 
                    (InfoOptions options) => new Info(options).Run(), 
                    (SetOptions options) => new Set(options).Run(),
                    (LoadOptions options) => new Load(options).Run(),
                    (BatchOptions options) => new Batch(options).Run(),
                    (ProducerOptions options) => new Producer(options).Run(),
                    (WorkerOptions options) => new Worker(options).Run(),
                    errs => Task.FromResult(1));

            Environment.ExitCode = result;
        }
    }

    public static class CmdLineExtensions
    {
        public static ParserResult<T> ThrowOnParseError<T>(this ParserResult<T> result)
        {
            if (!(result is NotParsed<T>))
            {
                // Case with no errors needs to be detected explicitly, otherwise the .Select line will throw an InvalidCastException
                return result;
            }

            var builder = SentenceBuilder.Create();
            var errorMessages = HelpText.RenderParsingErrorsTextAsLines(result, builder.FormatError, builder.FormatMutuallyExclusiveSetErrors, 1);

            var excList = errorMessages.Select(msg => new ArgumentException(msg)).ToList();

            if (excList.Any())
            {
                throw new AggregateException(excList);
            }

            return result;
        }        
    }
}