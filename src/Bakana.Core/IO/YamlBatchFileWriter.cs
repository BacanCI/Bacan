using System;
using System.IO.Abstractions;
using Bakana.DomainModels;
using YamlDotNet.Serialization;

namespace Bakana.Core.IO
{
    public class YamlBatchFileWriter : IBatchFileWriter
    {
        private readonly IFileSystem fileSystem;
        private readonly Lazy<ISerializer> serializer;

        public static Func<SerializerBuilder> SerializerBuilderFn = () => new SerializerBuilder()
            .DisableAliases();

        public YamlBatchFileWriter() : this(new FileSystem())
        {
        }
        
        public YamlBatchFileWriter(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
            serializer = new Lazy<ISerializer>(SerializerBuilderFn().Build);
        }

        public void WriteFile(Batch batch, string path)
        {
            var yaml = serializer.Value.Serialize(batch);
            fileSystem.File.WriteAllText(path, yaml);
        }
    }
}