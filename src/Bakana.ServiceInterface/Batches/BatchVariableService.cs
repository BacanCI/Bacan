using System.Collections.Generic;
using System.Threading.Tasks;
using Bakana.Core.Entities;
using Bakana.Core.Repositories;
using Bakana.ServiceModels.Batches;
using ServiceStack;

namespace Bakana.ServiceInterface.Batches
{
    public class BatchVariableService : Service
    {
        private readonly IBatchRepository batchRepository;

        public BatchVariableService(IBatchRepository batchRepository)
        {
            this.batchRepository = batchRepository;
        }
        
        public async Task<CreateBatchVariableResponse> Post(CreateBatchVariableRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            if (await batchRepository.DoesBatchVariableExist(request.BatchId, request.VariableId))
                throw Err.BatchVariableAlreadyExists(request.VariableId);

            var batchVariable = request.ConvertTo<BatchVariable>();

            await batchRepository.CreateOrUpdateBatchVariable(batchVariable);

            return new CreateBatchVariableResponse();
        }

        public async Task<GetBatchVariableResponse> Get(GetBatchVariableRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var batchVariable = await batchRepository.GetBatchVariable(request.BatchId, request.VariableId);
            if (batchVariable == null)
                throw Err.BatchVariableNotFound(request.VariableId);

            return batchVariable.ConvertTo<GetBatchVariableResponse>();
        }

        public async Task<GetAllBatchVariableResponse> Get(GetAllBatchVariableRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var batchVariables = await batchRepository.GetAllBatchVariables(request.BatchId);
            var response = new GetAllBatchVariableResponse
            {
                Variables = batchVariables.ConvertTo<List<ServiceModels.Variable>>()
            };

            return response;
        }

        public async Task<UpdateBatchVariableResponse> Put(UpdateBatchVariableRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var existingBatchVariable =
                await batchRepository.GetBatchVariable(request.BatchId, request.VariableId);
            if (existingBatchVariable == null)
                throw Err.BatchVariableNotFound(request.VariableId);

            var batchVariable = request.ConvertTo<BatchVariable>();
            batchVariable.Id = existingBatchVariable.Id;

            await batchRepository.CreateOrUpdateBatchVariable(batchVariable);

            return new UpdateBatchVariableResponse();
        }
        
        public async Task<DeleteBatchVariableResponse> Delete(DeleteBatchVariableRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var existingBatchVariable =
                await batchRepository.GetBatchVariable(request.BatchId, request.VariableId);
            if (existingBatchVariable == null)
                throw Err.BatchVariableNotFound(request.VariableId);

            await batchRepository.DeleteBatchVariable(existingBatchVariable.Id);

            return new DeleteBatchVariableResponse();
        }

    }
}