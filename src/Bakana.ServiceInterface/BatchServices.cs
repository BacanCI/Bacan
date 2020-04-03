using System;
using Bakana.Core;
using Bakana.Core.Entities;
using Bakana.Core.Repositories;
using Bakana.ServiceModels;
using ServiceStack;
using System.Threading.Tasks;

namespace Bakana.ServiceInterface
{
    public class BatchServices : Service
    {
        private readonly IShortIdGenerator idGenerator;
        private readonly IBatchRepository batchRepository;

        public BatchServices(
            IShortIdGenerator idGenerator, 
            IBatchRepository batchRepository)
        {
            this.idGenerator = idGenerator;
            this.batchRepository = batchRepository;
        }
        
        public async Task<CreateBatchResponse> Post(CreateBatchRequest request)
        {
            var batch = request.ConvertTo<Batch>();
            batch.Id = idGenerator.Generate();
            batch.CreatedOn = DateTime.UtcNow;

            await batchRepository.Create(batch);

            return new CreateBatchResponse
            {
                BatchId = batch.Id
            };
        }

        public async Task<GetBatchResponse> Get(GetBatchRequest request)
        {
            var batch = await batchRepository.Get(request.BatchId);

            return batch.ConvertTo<GetBatchResponse>();
        }
    }
}
