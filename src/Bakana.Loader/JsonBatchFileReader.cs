using System.IO.Abstractions;
using Bakana.DomainModels;
using ServiceStack;

namespace Bakana.Loader
{
    public class JsonBatchFileReader : IBatchFileReader
    {
        private readonly IFileSystem fileSystem;

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
            return json.FromJson<Batch>();
        }
    }
}