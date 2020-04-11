using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using Bakana.Core.IO;
using Bakana.DomainModels;
using Bakana.TestData.IO;

namespace Bakana.UnitTests.IO
{
    public abstract class BatchFileTestFixtureBase
    {
        protected const string FullyPopulatedYamlFile = @"c:\fullyPopulatedBatch.yaml";
        protected const string FullyPopulatedJsonFile = @"c:\fullyPopulatedBatch.json";
        
        protected readonly MockFileSystem FileSystem;

        protected BatchFileTestFixtureBase()
        {
            FileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { FullyPopulatedYamlFile, new MockFileData(YamlSamples.FullyPopulatedBatch) },
                { FullyPopulatedJsonFile, new MockFileData(JsonSamples.FullyPopulatedBatch) },
            });
        }

        protected Batch ReadYamlFile(string path)
        {
            return new YamlBatchFileReader(FileSystem).ReadFile(path);
        }
        
        protected Batch ReadJsonFile(string path)
        {
            return new JsonBatchFileReader(FileSystem).ReadFile(path);
        }
    }
}