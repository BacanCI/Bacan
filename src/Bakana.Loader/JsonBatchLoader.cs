using System.IO.Abstractions;
using System.Threading.Tasks;
using Bakana.ServiceModels.Batches;
using ServiceStack;

namespace Bakana.Loader
{
    public class JsonBatchLoader : IBatchLoader
    {
        private readonly IFileSystem fileSystem;
        private readonly IServiceClient client;

        public JsonBatchLoader(string bakanaProducerUri) : this(new FileSystem(), bakanaProducerUri)
        {
        }
        
        public JsonBatchLoader(IFileSystem fileSystem, string bakanaProducerUri)
        {
            this.fileSystem = fileSystem;

            client = new JsonServiceClient(bakanaProducerUri);
        }

        public async Task<string> LoadBatch(string path)
        {
            var json = fileSystem.File.ReadAllText(path);
            var createBatchRequest = json.FromJson<CreateBatchRequest>();

            var response = await client.SendAsync(createBatchRequest);

            return response.BatchId;
        }
    }
}