using System.Collections.Generic;
using System.Threading.Tasks;
using Bakana.Core.Entities;
using Bakana.Core.Repositories;
using Bakana.ServiceModels.Batches;
using ServiceStack;

namespace Bakana.ServiceInterface.Batches
{
    public class BatchArtifactService : Service
    {
        private readonly IBatchRepository batchRepository;

        public BatchArtifactService(IBatchRepository batchRepository)
        {
            this.batchRepository = batchRepository;
        }

        public async Task<CreateBatchArtifactResponse> Post(CreateBatchArtifactRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            if (await batchRepository.DoesBatchArtifactExist(request.BatchId, request.ArtifactName))
                throw Err.BatchArtifactAlreadyExists(request.ArtifactName);

            var batchArtifact = request.ConvertTo<BatchArtifact>();

            await batchRepository.CreateBatchArtifact(batchArtifact);

            return new CreateBatchArtifactResponse();
        }

        public async Task<GetBatchArtifactResponse> Get(GetBatchArtifactRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var batchArtifact = await batchRepository.GetBatchArtifact(request.BatchId, request.ArtifactName);
            if (batchArtifact == null)
                throw Err.BatchArtifactNotFound(request.ArtifactName);

            return batchArtifact.ConvertTo<GetBatchArtifactResponse>();
        }

        public async Task<GetAllBatchArtifactResponse> Get(GetAllBatchArtifactRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var batchArtifacts = await batchRepository.GetAllBatchArtifacts(request.BatchId);
            var response = new GetAllBatchArtifactResponse
            {
                Artifacts = batchArtifacts.ConvertTo<List<ServiceModels.BatchArtifact>>()
            };

            return response;
        }

        public async Task<UpdateBatchArtifactResponse> Put(UpdateBatchArtifactRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var existingBatchArtifact =
                await batchRepository.GetBatchArtifact(request.BatchId, request.ArtifactName);
            if (existingBatchArtifact == null)
                throw Err.BatchArtifactNotFound(request.ArtifactName);

            var batchArtifact = request.ConvertTo<BatchArtifact>();
            batchArtifact.Id = existingBatchArtifact.Id;

            await batchRepository.UpdateBatchArtifact(batchArtifact);

            return new UpdateBatchArtifactResponse();
        }
        
        public async Task<DeleteBatchArtifactResponse> Delete(DeleteBatchArtifactRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var existingBatchArtifact =
                await batchRepository.GetBatchArtifact(request.BatchId, request.ArtifactName);
            if (existingBatchArtifact == null)
                throw Err.BatchArtifactNotFound(request.ArtifactName);

            await batchRepository.DeleteBatchArtifact(existingBatchArtifact.Id);

            return new DeleteBatchArtifactResponse();
        }

        public async Task<CreateBatchArtifactOptionResponse> Post(CreateBatchArtifactOptionRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var batchArtifact = await batchRepository.GetBatchArtifact(request.BatchId, request.ArtifactName);
            if (batchArtifact == null)
                throw Err.BatchArtifactNotFound(request.ArtifactName);

            if (await batchRepository.DoesBatchArtifactOptionExist(request.BatchId, request.ArtifactName, request.OptionName)) 
                throw Err.BatchArtifactOptionAlreadyExists(request.OptionName);
            
            var batchArtifactOption = request.ConvertTo<BatchArtifactOption>();
            batchArtifactOption.BatchArtifactId = batchArtifact.Id;

            await batchRepository.CreateOrUpdateBatchArtifactOption(batchArtifactOption);

            return new CreateBatchArtifactOptionResponse();
        }

        public async Task<GetBatchArtifactOptionResponse> Get(GetBatchArtifactOptionRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var batchArtifact = await batchRepository.GetBatchArtifact(request.BatchId, request.ArtifactName);
            if (batchArtifact == null)
                throw Err.BatchArtifactNotFound(request.ArtifactName);

            var batchArtifactOption = await batchRepository.GetBatchArtifactOption(batchArtifact.Id, request.OptionName);
            if (batchArtifactOption == null)
                throw Err.BatchArtifactOptionNotFound(request.OptionName);

            return batchArtifactOption.ConvertTo<GetBatchArtifactOptionResponse>();
        }

        public async Task<GetAllBatchArtifactOptionResponse> Get(GetAllBatchArtifactOptionRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var batchArtifact = await batchRepository.GetBatchArtifact(request.BatchId, request.ArtifactName);
            if (batchArtifact == null)
                throw Err.BatchArtifactNotFound(request.ArtifactName);

            var response = new GetAllBatchArtifactOptionResponse
            {
                Options = batchArtifact.Options.ConvertTo<List<ServiceModels.Option>>()
            };

            return response;
        }

        public async Task<UpdateBatchArtifactOptionResponse> Put(UpdateBatchArtifactOptionRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var batchArtifact = await batchRepository.GetBatchArtifact(request.BatchId, request.ArtifactName);
            if (batchArtifact == null)
                throw Err.BatchArtifactNotFound(request.ArtifactName);

            var existingBatchArtifactOption =
                await batchRepository.GetBatchArtifactOption(batchArtifact.Id, request.OptionName);
            if (existingBatchArtifactOption == null)
                throw Err.BatchArtifactOptionNotFound(request.OptionName);

            var batchArtifactOption = request.ConvertTo<BatchArtifactOption>();
            batchArtifactOption.Id = existingBatchArtifactOption.Id;
            batchArtifactOption.BatchArtifactId = batchArtifact.Id;

            await batchRepository.CreateOrUpdateBatchArtifactOption(batchArtifactOption);

            return new UpdateBatchArtifactOptionResponse();
        }
        
        public async Task<DeleteBatchArtifactOptionResponse> Delete(DeleteBatchArtifactOptionRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var batchArtifact = await batchRepository.GetBatchArtifact(request.BatchId, request.ArtifactName);
            if (batchArtifact == null)
                throw Err.BatchArtifactNotFound(request.ArtifactName);

            var existingBatchArtifactOption =
                await batchRepository.GetBatchArtifactOption(batchArtifact.Id, request.OptionName);
            if (existingBatchArtifactOption == null)
                throw Err.BatchArtifactOptionNotFound(request.OptionName);

            await batchRepository.DeleteBatchArtifactOption(existingBatchArtifactOption.Id);

            return new DeleteBatchArtifactOptionResponse();
        }
    }
}