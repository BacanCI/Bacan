using System.Threading.Tasks;
using Bakana.Core.Entities;
using Bakana.ServiceModels.Batches;
using ServiceStack;

namespace Bakana.Core
{
    public class BakanaLoader : IBakanaLoader
    {
        private readonly IServiceClient client;
        
        public BakanaLoader(string host)
        {
            client = new JsonServiceClient(host);
        }

        public async Task<string> LoadBatch(Batch batch)
        {
            var request = batch.ConvertTo<CreateBatchRequest>();

            var response = await client.SendAsync(request);

            return response.BatchId;
        }
    }
}