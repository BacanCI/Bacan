using System;
using System.IO.Abstractions;
using Bakana.Core.Entities;
using YamlDotNet.Serialization;

namespace Bakana.Core.IO
{
    public class YamlBatchFileReader : IBatchFileReader
    {
        private readonly IFileSystem fileSystem;
        private readonly Lazy<IDeserializer> deserializer;

        public static Func<DeserializerBuilder> DeserializerBuilderFn = () => new DeserializerBuilder();

        public YamlBatchFileReader() : this(new FileSystem())
        {
        }
        
        public YamlBatchFileReader(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
            deserializer = new Lazy<IDeserializer>(DeserializerBuilderFn().Build);
        }

        public Batch ReadFile(string path)
        {
            var yaml = fileSystem.File.ReadAllText(path);

            return deserializer.Value.Deserialize<Batch>(yaml); 
        }
    }
}