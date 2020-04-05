using System.Collections.Generic;
using System.Threading.Tasks;
using Bakana.Core.Entities;
using Bakana.Core.Repositories;
using Bakana.ServiceModels.Steps;
using ServiceStack;

namespace Bakana.ServiceInterface.Steps
{
    public class StepArtifactService : BakanaService
    {
        private readonly IBatchRepository batchRepository;
        private readonly IStepRepository stepRepository;

        public StepArtifactService(
            IBatchRepository batchRepository,
            IStepRepository stepRepository)
        {
            this.batchRepository = batchRepository;
            this.stepRepository = stepRepository;
        }

        public async Task<CreateStepArtifactResponse> Post(CreateStepArtifactRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepId);
            if (step == null)
                throw StepNotFound(request.StepId);

            if (await stepRepository.DoesStepArtifactExist(request.BatchId, request.StepId, request.ArtifactId))
                throw StepArtifactAlreadyExists(request.ArtifactId);

            var stepArtifact = request.ConvertTo<StepArtifact>();
            stepArtifact.StepId = step.Id;

            await stepRepository.CreateStepArtifact(stepArtifact);

            return new CreateStepArtifactResponse();
        }

        public async Task<GetStepArtifactResponse> Get(GetStepArtifactRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepId);
            if (step == null)
                throw StepNotFound(request.StepId);

            var stepArtifact = await stepRepository.GetStepArtifact(step.Id, request.ArtifactId);
            if (stepArtifact == null)
                throw StepArtifactNotFound(request.ArtifactId);

            return stepArtifact.ConvertTo<GetStepArtifactResponse>();
        }

        public async Task<GetAllStepArtifactResponse> Get(GetAllStepArtifactRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepId);
            if (step == null)
                throw StepNotFound(request.StepId);

            var stepArtifacts = await stepRepository.GetAllStepArtifacts(step.Id);
            var response = new GetAllStepArtifactResponse
            {
                Artifacts = stepArtifacts.ConvertTo<List<ServiceModels.StepArtifact>>()
            };

            return response;
        }

        public async Task<UpdateStepArtifactResponse> Put(UpdateStepArtifactRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepId);
            if (step == null)
                throw StepNotFound(request.StepId);

            var existingStepArtifact =
                await stepRepository.GetStepArtifact(step.Id, request.ArtifactId);
            if (existingStepArtifact == null)
                throw StepArtifactNotFound(request.ArtifactId);

            var stepArtifact = request.ConvertTo<StepArtifact>();
            stepArtifact.Id = existingStepArtifact.Id;

            await stepRepository.UpdateStepArtifact(stepArtifact);

            return new UpdateStepArtifactResponse();
        }
        
        public async Task<DeleteStepArtifactResponse> Delete(DeleteStepArtifactRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepId);
            if (step == null)
                throw StepNotFound(request.StepId);

            var existingStepArtifact =
                await stepRepository.GetStepArtifact(step.Id, request.ArtifactId);
            if (existingStepArtifact == null)
                throw StepArtifactNotFound(request.ArtifactId);

            await stepRepository.DeleteStepArtifact(existingStepArtifact.Id);

            return new DeleteStepArtifactResponse();
        }

        public async Task<CreateStepArtifactOptionResponse> Post(CreateStepArtifactOptionRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepId);
            if (step == null)
                throw StepNotFound(request.StepId);

            var existingStepArtifact =
                await stepRepository.GetStepArtifact(step.Id, request.ArtifactId);
            if (existingStepArtifact == null)
                throw StepArtifactNotFound(request.ArtifactId);

            if (await stepRepository.DoesStepArtifactOptionExist(request.BatchId, request.StepId, request.ArtifactId, request.OptionId))
                throw StepArtifactOptionAlreadyExists(request.ArtifactId);
            
            var stepArtifactOption = request.ConvertTo<StepArtifactOption>();
            stepArtifactOption.StepArtifactId = existingStepArtifact.Id;

            await stepRepository.CreateOrUpdateStepArtifactOption(stepArtifactOption);

            return new CreateStepArtifactOptionResponse();
        }

        public async Task<GetStepArtifactOptionResponse> Get(GetStepArtifactOptionRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepId);
            if (step == null)
                throw StepNotFound(request.StepId);

            var existingStepArtifact =
                await stepRepository.GetStepArtifact(step.Id, request.ArtifactId);
            if (existingStepArtifact == null)
                throw StepArtifactNotFound(request.ArtifactId);

            var stepArtifactOption = await stepRepository.GetStepArtifactOption(existingStepArtifact.Id, request.OptionId);
            if (stepArtifactOption == null)
                throw StepArtifactOptionNotFound(request.OptionId);

            return stepArtifactOption.ConvertTo<GetStepArtifactOptionResponse>();
        }

        public async Task<GetAllStepArtifactOptionResponse> Get(GetAllStepArtifactOptionRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepId);
            if (step == null)
                throw StepNotFound(request.StepId);

            var existingStepArtifact =
                await stepRepository.GetStepArtifact(step.Id, request.ArtifactId);
            if (existingStepArtifact == null)
                throw StepArtifactNotFound(request.ArtifactId);

            var response = new GetAllStepArtifactOptionResponse
            {
                Options = existingStepArtifact.Options.ConvertTo<List<ServiceModels.Option>>()
            };

            return response;
        }

        public async Task<UpdateStepArtifactOptionResponse> Put(UpdateStepArtifactOptionRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepId);
            if (step == null)
                throw StepNotFound(request.StepId);

            var existingStepArtifact =
                await stepRepository.GetStepArtifact(step.Id, request.ArtifactId);
            if (existingStepArtifact == null)
                throw StepArtifactNotFound(request.ArtifactId);

            var existingStepArtifactOption = await stepRepository.GetStepArtifactOption(existingStepArtifact.Id, request.OptionId);
            if (existingStepArtifactOption == null)
                throw StepArtifactOptionNotFound(request.OptionId);

            var stepArtifactOption = request.ConvertTo<StepArtifactOption>();
            stepArtifactOption.Id = existingStepArtifactOption.Id;

            await stepRepository.CreateOrUpdateStepArtifactOption(stepArtifactOption);

            return new UpdateStepArtifactOptionResponse();
        }
        
        public async Task<DeleteStepArtifactOptionResponse> Delete(DeleteStepArtifactOptionRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepId);
            if (step == null)
                throw StepNotFound(request.StepId);

            var existingStepArtifact =
                await stepRepository.GetStepArtifact(step.Id, request.ArtifactId);
            if (existingStepArtifact == null)
                throw StepArtifactNotFound(request.ArtifactId);

            var existingStepArtifactOption = await stepRepository.GetStepArtifactOption(existingStepArtifact.Id, request.OptionId);
            if (existingStepArtifactOption == null)
                throw StepArtifactOptionNotFound(request.OptionId);

            await stepRepository.DeleteStepArtifact(existingStepArtifactOption.Id);

            return new DeleteStepArtifactOptionResponse();
        }
    }
}