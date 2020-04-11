using System;
using System.IO.Abstractions;
using Bakana.Core.Entities;
using ServiceStack.Text;

namespace Bakana.Core.IO
{
    public class JsonBatchFileWriter : IBatchFileWriter
    {
        private readonly IFileSystem fileSystem;

        public static Func<Batch, string> JsonSerializerFn =
            batch => JsonSerializer.SerializeToString(batch).IndentJson();

        public JsonBatchFileWriter() : this(new FileSystem())
        {
        }
        
        public JsonBatchFileWriter(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public void WriteFile(Batch batch, string path)
        {
            var json = JsonSerializerFn(batch);
            fileSystem.File.WriteAllText(path, json);
        }
    }
}
