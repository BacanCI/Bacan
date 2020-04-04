using System.Collections.Generic;
using System.Threading.Tasks;
using Bakana.Core.Entities;
using Bakana.Core.Repositories;
using Bakana.ServiceModels.Batches;
using ServiceStack;

namespace Bakana.ServiceInterface.Batches
{
    public class BatchOptionService : BakanaService
    {
        private readonly IBatchRepository batchRepository;

        public BatchOptionService(IBatchRepository batchRepository)
        {
            this.batchRepository = batchRepository;
        }

        public async Task<CreateBatchOptionResponse> Post(CreateBatchOptionRequest request)
        {
            if (!await batchRepository.DoesExist(request.BatchId)) 
                throw BatchNotFound(request.BatchId);

            if (await batchRepository.DoesBatchOptionExist(request.BatchId, request.OptionId))
                throw BatchOptionAlreadyExists(request.OptionId);

            var batchOption = request.ConvertTo<BatchOption>();

            await batchRepository.CreateOrUpdateBatchOption(batchOption);

            return new CreateBatchOptionResponse();
        }

        public async Task<GetBatchOptionResponse> Get(GetBatchOptionRequest request)
        {
            if (!await batchRepository.DoesExist(request.BatchId)) 
                throw BatchNotFound(request.BatchId);

            var batchOption = await batchRepository.GetBatchOption(request.BatchId, request.OptionId);
            if (batchOption == null)
                throw BatchOptionNotFound(request.OptionId);

            return batchOption.ConvertTo<GetBatchOptionResponse>();
        }

        public async Task<GetAllBatchOptionResponse> Get(GetAllBatchOptionRequest request)
        {
            if (!await batchRepository.DoesExist(request.BatchId)) 
                throw BatchNotFound(request.BatchId);

            var batchOptions = await batchRepository.GetAllBatchOptions(request.BatchId);
            var response = new GetAllBatchOptionResponse
            {
                Options = batchOptions.ConvertTo<List<ServiceModels.Option>>()
            };

            return response;
        }

        public async Task<UpdateBatchOptionResponse> Put(UpdateBatchOptionRequest request)
        {
            if (!await batchRepository.DoesExist(request.BatchId)) 
                throw BatchNotFound(request.BatchId);

            var existingBatchOption =
                await batchRepository.GetBatchOption(request.BatchId, request.OptionId);
            if (existingBatchOption == null)
                throw BatchOptionNotFound(request.OptionId);

            var batchOption = request.ConvertTo<BatchOption>();
            batchOption.Id = existingBatchOption.Id;

            await batchRepository.CreateOrUpdateBatchOption(batchOption);

            return new UpdateBatchOptionResponse();
        }
        
        public async Task<DeleteBatchOptionResponse> Delete(DeleteBatchOptionRequest request)
        {
            if (!await batchRepository.DoesExist(request.BatchId)) 
                throw BatchNotFound(request.BatchId);

            var existingBatchOption =
                await batchRepository.GetBatchOption(request.BatchId, request.OptionId);
            if (existingBatchOption == null)
                throw BatchOptionNotFound(request.OptionId);

            await batchRepository.DeleteBatchOption(existingBatchOption.Id);

            return new DeleteBatchOptionResponse();
        }

    }
}