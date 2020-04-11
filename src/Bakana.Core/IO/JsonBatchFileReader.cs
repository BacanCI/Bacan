using System;
using System.IO.Abstractions;
using Bakana.DomainModels;
using ServiceStack.Text;

namespace Bakana.Core.IO
{
    public class JsonBatchFileReader : IBatchFileReader
    {
        private readonly IFileSystem fileSystem;

        public static Func<string, Batch> JsonSerializerFn =
            JsonSerializer.DeserializeFromString<Batch>;

        public JsonBatchFileReader() : this(new FileSystem())
        {
        }
        
        public JsonBatchFileReader(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public Batch ReadFile(string path)
        {
            var json = fileSystem.File.ReadAllText(path);
            return JsonSerializerFn(json); 
        }
    }
}
