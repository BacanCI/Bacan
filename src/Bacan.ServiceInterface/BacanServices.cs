using Bacan.Core;
using ServiceStack;

namespace Bacan.ServiceInterface
{
    public class BacanServices : Service
    {
        public CreateBatchResponse Post(CreateBatch batch)
        {
            foreach (var createBatchJob in batch.Jobs)
            {
                PublishMessage(createBatchJob);
            }
            
            return new CreateBatchResponse
            {
                Id = batch.Id
            };
        }
    }
}
