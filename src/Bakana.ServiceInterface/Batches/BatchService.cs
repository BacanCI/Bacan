using System;
using System.Threading.Tasks;
using Bakana.Core;
using Bakana.Core.Entities;
using Bakana.Core.Repositories;
using Bakana.ServiceModels.Batches;
using ServiceStack;

namespace Bakana.ServiceInterface.Batches
{
    public class BatchService : Service
    {
        private readonly IShortIdGenerator idGenerator;
        private readonly IBatchRepository batchRepository;

        public BatchService(
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
            if (batch == null) throw Err.BatchNotFound(request.BatchId);

            return batch.ConvertTo<GetBatchResponse>();
        }
        
        public async Task<UpdateBatchResponse> Put(UpdateBatchRequest request)
        {
            var batch = request.ConvertTo<Batch>();

            var updated = await batchRepository.Update(batch);
            if (!updated) throw Err.BatchNotFound(request.BatchId);

            return new UpdateBatchResponse();
        }
        
        public async Task<DeleteBatchResponse> Delete(DeleteBatchRequest request)
        {
            var deleted = await batchRepository.Delete(request.BatchId);
            if (!deleted) throw Err.BatchNotFound(request.BatchId);

            return new DeleteBatchResponse();
        }
    }
}
