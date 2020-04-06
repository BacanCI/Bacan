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

            var step = await stepRepository.Get(request.BatchId, request.StepId);
            if (step == null)
                throw Err.StepNotFound(request.StepId);

            if (await stepRepository.DoesStepOptionExist(request.BatchId, request.StepId, request.OptionId))
                throw Err.StepOptionAlreadyExists(request.OptionId);

            var stepOption = request.ConvertTo<StepOption>();
            stepOption.StepId = step.Id;

            await stepRepository.CreateOrUpdateStepOption(stepOption);

            return new CreateStepOptionResponse();
        }

        public async Task<GetStepOptionResponse> Get(GetStepOptionRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepId);
            if (step == null)
                throw Err.StepNotFound(request.StepId);

            var stepOption = await stepRepository.GetStepOption(step.Id, request.OptionId);
            if (stepOption == null)
                throw Err.StepOptionNotFound(request.OptionId);

            return stepOption.ConvertTo<GetStepOptionResponse>();
        }

        public async Task<GetAllStepOptionResponse> Get(GetAllStepOptionRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepId);
            if (step == null)
                throw Err.StepNotFound(request.StepId);

            var response = new GetAllStepOptionResponse
            {
                Options = step.Options.ConvertTo<List<ServiceModels.Option>>()
            };

            return response;
        }

        public async Task<UpdateStepOptionResponse> Put(UpdateStepOptionRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepId);
            if (step == null)
                throw Err.StepNotFound(request.StepId);

            var existingStepOption =
                await stepRepository.GetStepOption(step.Id, request.OptionId);
            if (existingStepOption == null)
                throw Err.StepOptionNotFound(request.OptionId);

            var stepOption = request.ConvertTo<StepOption>();
            stepOption.Id = existingStepOption.Id;

            await stepRepository.CreateOrUpdateStepOption(stepOption);

            return new UpdateStepOptionResponse();
        }
        
        public async Task<DeleteStepOptionResponse> Delete(DeleteStepOptionRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepId);
            if (step == null)
                throw Err.StepNotFound(request.StepId);

            var existingStepOption =
                await stepRepository.GetStepOption(step.Id, request.OptionId);
            if (existingStepOption == null)
                throw Err.StepOptionNotFound(request.OptionId);

            await stepRepository.DeleteStepOption(existingStepOption.Id);

            return new DeleteStepOptionResponse();
        }
    }
}
