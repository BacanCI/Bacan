using System.Collections.Generic;
using System.Threading.Tasks;
using Bakana.Core.Entities;
using Bakana.Core.Repositories;
using Bakana.ServiceModels.Steps;
using ServiceStack;

namespace Bakana.ServiceInterface.Steps
{
    public class StepOptionService : Service
    {
        private readonly IBatchRepository batchRepository;
        private readonly IStepRepository stepRepository;

        public StepOptionService(
            IBatchRepository batchRepository,
            IStepRepository stepRepository)
        {
            this.batchRepository = batchRepository;
            this.stepRepository = stepRepository;
        }

        public async Task<CreateStepOptionResponse> Post(CreateStepOptionRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepName);
            if (step == null)
                throw Err.StepNotFound(request.StepName);

            if (await stepRepository.DoesStepOptionExist(request.BatchId, request.StepName, request.OptionName))
                throw Err.StepOptionAlreadyExists(request.OptionName);

            var stepOption = request.ConvertTo<StepOption>();
            stepOption.StepId = step.Id;

            await stepRepository.CreateOrUpdateStepOption(stepOption);

            return new CreateStepOptionResponse();
        }

        public async Task<GetStepOptionResponse> Get(GetStepOptionRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepName);
            if (step == null)
                throw Err.StepNotFound(request.StepName);

            var stepOption = await stepRepository.GetStepOption(step.Id, request.OptionName);
            if (stepOption == null)
                throw Err.StepOptionNotFound(request.OptionName);

            return stepOption.ConvertTo<GetStepOptionResponse>();
        }

        public async Task<GetAllStepOptionResponse> Get(GetAllStepOptionRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepName);
            if (step == null)
                throw Err.StepNotFound(request.StepName);

            var response = new GetAllStepOptionResponse
            {
                Options = step.Options.ConvertTo<List<DomainModels.Option>>()
            };

            return response;
        }

        public async Task<UpdateStepOptionResponse> Put(UpdateStepOptionRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepName);
            if (step == null)
                throw Err.StepNotFound(request.StepName);

            var existingStepOption =
                await stepRepository.GetStepOption(step.Id, request.OptionName);
            if (existingStepOption == null)
                throw Err.StepOptionNotFound(request.OptionName);

            var stepOption = request.ConvertTo<StepOption>();
            stepOption.Id = existingStepOption.Id;
            stepOption.StepId = step.Id;

            await stepRepository.CreateOrUpdateStepOption(stepOption);

            return new UpdateStepOptionResponse();
        }
        
        public async Task<DeleteStepOptionResponse> Delete(DeleteStepOptionRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepName);
            if (step == null)
                throw Err.StepNotFound(request.StepName);

            var existingStepOption =
                await stepRepository.GetStepOption(step.Id, request.OptionName);
            if (existingStepOption == null)
                throw Err.StepOptionNotFound(request.OptionName);

            await stepRepository.DeleteStepOption(existingStepOption.Id);

            return new DeleteStepOptionResponse();
        }
    }
}
