using System;
using McMaster.Extensions.CommandLineUtils;
using ServiceStack.Text;

namespace Bakana
{
    public class Program
    {
        [Option("-M|--Mode", CommandOptionType.SingleValue, Description = "The mode of operation",
        ValueName = "Loader|Producer|Tracker|Worker|Client|All")]
        public Mode Mode { get; set; } = Mode.Loader;
        
        [Option("-H|--Host", CommandOptionType.SingleValue, Description = "Producer host url")]
        public string Host { get; set; }
        
        [Option("-B|--Batch", CommandOptionType.SingleValue, Description = "Batch file path")]
        public string Batch { get; set; }
        
        [Option("-F|--Format", CommandOptionType.SingleValue, Description = "Batch file format",
            ValueName = "Json|Yaml")]
        public BatchFileFormat Format { get; set; } = BatchFileFormat.Json;
        
        public void OnExecute()
        {
            Console.WriteLine(this.Dump());
        }
        
        public static int Main(string[] args) => CommandLineApplication.Execute<Program>(new []
        {
            "--Mode=loader",
            "--Host=https://localhost:5001/",
            "--Batch=c:\\temp\\batch.json",
            "--Format=json"
        });
    }
}