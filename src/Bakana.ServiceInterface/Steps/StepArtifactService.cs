using System.Collections.Generic;
using System.Threading.Tasks;
using Bakana.Core.Entities;
using Bakana.Core.Repositories;
using Bakana.ServiceModels.Steps;
using ServiceStack;

namespace Bakana.ServiceInterface.Steps
{
    public class StepArtifactService : Service
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
                throw Err.BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepName);
            if (step == null)
                throw Err.StepNotFound(request.StepName);

            if (await stepRepository.DoesStepArtifactExist(request.BatchId, request.StepName, request.ArtifactName))
                throw Err.StepArtifactAlreadyExists(request.ArtifactName);

            var stepArtifact = request.ConvertTo<StepArtifact>();
            stepArtifact.StepId = step.Id;

            await stepRepository.CreateStepArtifact(stepArtifact);

            return new CreateStepArtifactResponse();
        }

        public async Task<GetStepArtifactResponse> Get(GetStepArtifactRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepName);
            if (step == null)
                throw Err.StepNotFound(request.StepName);

            var stepArtifact = await stepRepository.GetStepArtifact(step.Id, request.ArtifactName);
            if (stepArtifact == null)
                throw Err.StepArtifactNotFound(request.ArtifactName);

            return stepArtifact.ConvertTo<GetStepArtifactResponse>();
        }

        public async Task<GetAllStepArtifactResponse> Get(GetAllStepArtifactRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepName);
            if (step == null)
                throw Err.StepNotFound(request.StepName);

            var stepArtifacts = await stepRepository.GetAllStepArtifacts(step.Id);
            var response = new GetAllStepArtifactResponse
            {
                Artifacts = stepArtifacts.ConvertTo<List<DomainModels.StepArtifact>>()
            };

            return response;
        }

        public async Task<UpdateStepArtifactResponse> Put(UpdateStepArtifactRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepName);
            if (step == null)
                throw Err.StepNotFound(request.StepName);

            var existingStepArtifact =
                await stepRepository.GetStepArtifact(step.Id, request.ArtifactName);
            if (existingStepArtifact == null)
                throw Err.StepArtifactNotFound(request.ArtifactName);

            var stepArtifact = request.ConvertTo<StepArtifact>();
            stepArtifact.StepId = step.Id;
            stepArtifact.Id = existingStepArtifact.Id;

            await stepRepository.UpdateStepArtifact(stepArtifact);

            return new UpdateStepArtifactResponse();
        }
        
        public async Task<DeleteStepArtifactResponse> Delete(DeleteStepArtifactRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepName);
            if (step == null)
                throw Err.StepNotFound(request.StepName);

            var existingStepArtifact =
                await stepRepository.GetStepArtifact(step.Id, request.ArtifactName);
            if (existingStepArtifact == null)
                throw Err.StepArtifactNotFound(request.ArtifactName);

            await stepRepository.DeleteStepArtifact(existingStepArtifact.Id);

            return new DeleteStepArtifactResponse();
        }

        public async Task<CreateStepArtifactOptionResponse> Post(CreateStepArtifactOptionRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepName);
            if (step == null)
                throw Err.StepNotFound(request.StepName);

            var existingStepArtifact =
                await stepRepository.GetStepArtifact(step.Id, request.ArtifactName);
            if (existingStepArtifact == null)
                throw Err.StepArtifactNotFound(request.ArtifactName);

            if (await stepRepository.DoesStepArtifactOptionExist(request.BatchId, request.StepName, request.ArtifactName, request.OptionName))
                throw Err.StepArtifactOptionAlreadyExists(request.OptionName);
            
            var stepArtifactOption = request.ConvertTo<StepArtifactOption>();
            stepArtifactOption.StepArtifactId = existingStepArtifact.Id;

            await stepRepository.CreateOrUpdateStepArtifactOption(stepArtifactOption);

            return new CreateStepArtifactOptionResponse();
        }

        public async Task<GetStepArtifactOptionResponse> Get(GetStepArtifactOptionRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepName);
            if (step == null)
                throw Err.StepNotFound(request.StepName);

            var existingStepArtifact =
                await stepRepository.GetStepArtifact(step.Id, request.ArtifactName);
            if (existingStepArtifact == null)
                throw Err.StepArtifactNotFound(request.ArtifactName);

            var stepArtifactOption = await stepRepository.GetStepArtifactOption(existingStepArtifact.Id, request.OptionName);
            if (stepArtifactOption == null)
                throw Err.StepArtifactOptionNotFound(request.OptionName);

            return stepArtifactOption.ConvertTo<GetStepArtifactOptionResponse>();
        }

        public async Task<GetAllStepArtifactOptionResponse> Get(GetAllStepArtifactOptionRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepName);
            if (step == null)
                throw Err.StepNotFound(request.StepName);

            var existingStepArtifact =
                await stepRepository.GetStepArtifact(step.Id, request.ArtifactName);
            if (existingStepArtifact == null)
                throw Err.StepArtifactNotFound(request.ArtifactName);

            var response = new GetAllStepArtifactOptionResponse
            {
                Options = existingStepArtifact.Options.ConvertTo<List<DomainModels.Option>>()
            };

            return response;
        }

        public async Task<UpdateStepArtifactOptionResponse> Put(UpdateStepArtifactOptionRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepName);
            if (step == null)
                throw Err.StepNotFound(request.StepName);

            var existingStepArtifact =
                await stepRepository.GetStepArtifact(step.Id, request.ArtifactName);
            if (existingStepArtifact == null)
                throw Err.StepArtifactNotFound(request.ArtifactName);

            var existingStepArtifactOption = await stepRepository.GetStepArtifactOption(existingStepArtifact.Id, request.OptionName);
            if (existingStepArtifactOption == null)
                throw Err.StepArtifactOptionNotFound(request.OptionName);

            var stepArtifactOption = request.ConvertTo<StepArtifactOption>();
            stepArtifactOption.Id = existingStepArtifactOption.Id;
            stepArtifactOption.StepArtifactId = existingStepArtifact.Id;

            await stepRepository.CreateOrUpdateStepArtifactOption(stepArtifactOption);

            return new UpdateStepArtifactOptionResponse();
        }
        
        public async Task<DeleteStepArtifactOptionResponse> Delete(DeleteStepArtifactOptionRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepName);
            if (step == null)
                throw Err.StepNotFound(request.StepName);

            var existingStepArtifact =
                await stepRepository.GetStepArtifact(step.Id, request.ArtifactName);
            if (existingStepArtifact == null)
                throw Err.StepArtifactNotFound(request.ArtifactName);

            var existingStepArtifactOption = await stepRepository.GetStepArtifactOption(existingStepArtifact.Id, request.OptionName);
            if (existingStepArtifactOption == null)
                throw Err.StepArtifactOptionNotFound(request.OptionName);

            await stepRepository.DeleteStepArtifactOption(existingStepArtifactOption.Id);

            return new DeleteStepArtifactOptionResponse();
        }
    }
}