using System;
using System.Collections.Generic;
using System.Net;
using Bakana.Core;
using Bakana.Core.Entities;
using Bakana.Core.Repositories;
using ServiceStack;
using System.Threading.Tasks;
using Bakana.ServiceModels.Batches;
using Option = Bakana.ServiceModels.Option;
using Variable = Bakana.ServiceModels.Variable;

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
            if (batch == null)
            {
                throw new HttpError(HttpStatusCode.NotFound, $"Batch {request.BatchId} not found");
            }

            return batch.ConvertTo<GetBatchResponse>();
        }
        
        public async Task<UpdateBatchResponse> Put(UpdateBatchRequest request)
        {
            var batch = request.ConvertTo<Batch>();

            var updated = await batchRepository.Update(batch);
            if (!updated)
            {
                throw new HttpError(HttpStatusCode.NotFound, $"Batch {request.BatchId} not found");
            }

            return new UpdateBatchResponse();
        }
        
        public async Task<DeleteBatchResponse> Delete(DeleteBatchRequest request)
        {
            var deleted = await batchRepository.Delete(request.BatchId);
            if (!deleted)
            {
                throw new HttpError(HttpStatusCode.NotFound, $"Batch {request.BatchId} not found");
            }

            return new DeleteBatchResponse();
        }
        
        
        public async Task<CreateBatchVariableResponse> Post(CreateBatchVariableRequest request)
        {
            if (!await batchRepository.DoesExist(request.BatchId))
            {
                throw new HttpError(HttpStatusCode.NotFound, $"Batch {request.BatchId} not found");
            }

            if (await batchRepository.DoesBatchVariableExist(request.BatchId, request.VariableId))
            {
                throw new HttpError(HttpStatusCode.Conflict, $"Variable {request.VariableId} already exists");
            }

            var batchVariable = request.ConvertTo<BatchVariable>();

            await batchRepository.CreateOrUpdateBatchVariable(batchVariable);

            return new CreateBatchVariableResponse();
        }

        public async Task<GetBatchVariableResponse> Get(GetBatchVariableRequest request)
        {
            if (!await batchRepository.DoesExist(request.BatchId))
            {
                throw new HttpError(HttpStatusCode.NotFound, $"Batch {request.BatchId} not found");
            }

            var batchVariable = await batchRepository.GetBatchVariable(request.BatchId, request.VariableId);
            if (batchVariable == null)
            {
                throw new HttpError(HttpStatusCode.NotFound, $"Batch variable {request.BatchId} not found");
            }

            return batchVariable.ConvertTo<GetBatchVariableResponse>();
        }

        public async Task<GetAllBatchVariableResponse> Get(GetAllBatchVariableRequest request)
        {
            if (!await batchRepository.DoesExist(request.BatchId))
            {
                throw new HttpError(HttpStatusCode.NotFound, $"Batch {request.BatchId} not found");
            }

            var batchVariables = await batchRepository.GetAllBatchVariables(request.BatchId);
            var response = new GetAllBatchVariableResponse
            {
                Variables = batchVariables.ConvertTo<List<Variable>>()
            };

            return response;
        }

        public async Task<UpdateBatchVariableResponse> Put(UpdateBatchVariableRequest request)
        {
            if (!await batchRepository.DoesExist(request.BatchId))
            {
                throw new HttpError(HttpStatusCode.NotFound, $"Batch {request.BatchId} not found");
            }

            var existingBatchVariable =
                await batchRepository.GetBatchVariable(request.BatchId, request.VariableId);
            if (existingBatchVariable == null)
            {
                throw new HttpError(HttpStatusCode.NotFound, $"Batch variable {request.VariableId} not found");
            }

            var batchVariable = request.ConvertTo<BatchVariable>();
            batchVariable.Id = existingBatchVariable.Id;

            await batchRepository.CreateOrUpdateBatchVariable(batchVariable);

            return new UpdateBatchVariableResponse();
        }
        
        public async Task<DeleteBatchVariableResponse> Delete(DeleteBatchVariableRequest request)
        {
            if (!await batchRepository.DoesExist(request.BatchId))
            {
                throw new HttpError(HttpStatusCode.NotFound, $"Batch {request.BatchId} not found");
            }

            var existingBatchVariable =
                await batchRepository.GetBatchVariable(request.BatchId, request.VariableId);
            if (existingBatchVariable == null)
            {
                throw new HttpError(HttpStatusCode.NotFound, $"Batch variable {request.VariableId} not found");
            }

            await batchRepository.DeleteBatchVariable(existingBatchVariable.Id);

            return new DeleteBatchVariableResponse();
        }
        
        
        
        public async Task<CreateBatchOptionResponse> Post(CreateBatchOptionRequest request)
        {
            if (!await batchRepository.DoesExist(request.BatchId))
            {
                throw new HttpError(HttpStatusCode.NotFound, $"Batch {request.BatchId} not found");
            }

            if (await batchRepository.DoesBatchOptionExist(request.BatchId, request.OptionId))
            {
                throw new HttpError(HttpStatusCode.Conflict, $"Option {request.OptionId} already exists");
            }

            var batchOption = request.ConvertTo<BatchOption>();

            await batchRepository.CreateOrUpdateBatchOption(batchOption);

            return new CreateBatchOptionResponse();
        }

        public async Task<GetBatchOptionResponse> Get(GetBatchOptionRequest request)
        {
            if (!await batchRepository.DoesExist(request.BatchId))
            {
                throw new HttpError(HttpStatusCode.NotFound, $"Batch {request.BatchId} not found");
            }

            var batchOption = await batchRepository.GetBatchOption(request.BatchId, request.OptionId);
            if (batchOption == null)
            {
                throw new HttpError(HttpStatusCode.NotFound, $"Batch Option {request.BatchId} not found");
            }

            return batchOption.ConvertTo<GetBatchOptionResponse>();
        }

        public async Task<GetAllBatchOptionResponse> Get(GetAllBatchOptionRequest request)
        {
            if (!await batchRepository.DoesExist(request.BatchId))
            {
                throw new HttpError(HttpStatusCode.NotFound, $"Batch {request.BatchId} not found");
            }

            var batchOptions = await batchRepository.GetAllBatchOptions(request.BatchId);
            var response = new GetAllBatchOptionResponse
            {
                Options = batchOptions.ConvertTo<List<Option>>()
            };

            return response;
        }

        public async Task<UpdateBatchOptionResponse> Put(UpdateBatchOptionRequest request)
        {
            if (!await batchRepository.DoesExist(request.BatchId))
            {
                throw new HttpError(HttpStatusCode.NotFound, $"Batch {request.BatchId} not found");
            }

            var existingBatchOption =
                await batchRepository.GetBatchOption(request.BatchId, request.OptionId);
            if (existingBatchOption == null)
            {
                throw new HttpError(HttpStatusCode.NotFound, $"Batch Option {request.OptionId} not found");
            }

            var batchOption = request.ConvertTo<BatchOption>();
            batchOption.Id = existingBatchOption.Id;

            await batchRepository.CreateOrUpdateBatchOption(batchOption);

            return new UpdateBatchOptionResponse();
        }
        
        public async Task<DeleteBatchOptionResponse> Delete(DeleteBatchOptionRequest request)
        {
            if (!await batchRepository.DoesExist(request.BatchId))
            {
                throw new HttpError(HttpStatusCode.NotFound, $"Batch {request.BatchId} not found");
            }

            var existingBatchOption =
                await batchRepository.GetBatchOption(request.BatchId, request.OptionId);
            if (existingBatchOption == null)
            {
                throw new HttpError(HttpStatusCode.NotFound, $"Batch Option {request.OptionId} not found");
            }

            await batchRepository.DeleteBatchOption(existingBatchOption.Id);

            return new DeleteBatchOptionResponse();
        }
    }
}
