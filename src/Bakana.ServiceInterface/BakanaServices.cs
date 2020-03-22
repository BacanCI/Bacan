using Bakana.Core;
using ServiceStack;

namespace Bakana.ServiceInterface
{
    public class BakanaServices : Service
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