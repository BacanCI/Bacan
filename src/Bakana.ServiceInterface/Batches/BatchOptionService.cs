using System.Collections.Generic;
using System.Threading.Tasks;
using Bakana.Core.Entities;
using Bakana.Core.Repositories;
using Bakana.ServiceModels.Batches;
using ServiceStack;

namespace Bakana.ServiceInterface.Batches
{
    public class BatchOptionService : Service
    {
        private readonly IBatchRepository batchRepository;

        public BatchOptionService(IBatchRepository batchRepository)
        {
            this.batchRepository = batchRepository;
        }

        public async Task<CreateBatchOptionResponse> Post(CreateBatchOptionRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            if (await batchRepository.DoesBatchOptionExist(request.BatchId, request.OptionName))
                throw Err.BatchOptionAlreadyExists(request.OptionName);

            var batchOption = request.ConvertTo<BatchOption>();

            await batchRepository.CreateOrUpdateBatchOption(batchOption);

            return new CreateBatchOptionResponse();
        }

        public async Task<GetBatchOptionResponse> Get(GetBatchOptionRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var batchOption = await batchRepository.GetBatchOption(request.BatchId, request.OptionName);
            if (batchOption == null)
                throw Err.BatchOptionNotFound(request.OptionName);

            return batchOption.ConvertTo<GetBatchOptionResponse>();
        }

        public async Task<GetAllBatchOptionResponse> Get(GetAllBatchOptionRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var batchOptions = await batchRepository.GetAllBatchOptions(request.BatchId);
            var response = new GetAllBatchOptionResponse
            {
                Options = batchOptions.ConvertTo<List<ServiceModels.Option>>()
            };

            return response;
        }

        public async Task<UpdateBatchOptionResponse> Put(UpdateBatchOptionRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var existingBatchOption =
                await batchRepository.GetBatchOption(request.BatchId, request.OptionName);
            if (existingBatchOption == null)
                throw Err.BatchOptionNotFound(request.OptionName);

            var batchOption = request.ConvertTo<BatchOption>();
            batchOption.Id = existingBatchOption.Id;

            await batchRepository.CreateOrUpdateBatchOption(batchOption);

            return new UpdateBatchOptionResponse();
        }
        
        public async Task<DeleteBatchOptionResponse> Delete(DeleteBatchOptionRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var existingBatchOption =
                await batchRepository.GetBatchOption(request.BatchId, request.OptionName);
            if (existingBatchOption == null)
                throw Err.BatchOptionNotFound(request.OptionName);

            await batchRepository.DeleteBatchOption(existingBatchOption.Id);

            return new DeleteBatchOptionResponse();
        }

    }
}