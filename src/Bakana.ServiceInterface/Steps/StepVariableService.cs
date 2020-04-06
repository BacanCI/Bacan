using System.Collections.Generic;
using System.Threading.Tasks;
using Bakana.Core.Entities;
using Bakana.Core.Repositories;
using Bakana.ServiceModels.Steps;
using ServiceStack;

namespace Bakana.ServiceInterface.Steps
{
    public class StepVariableService : Service
    {
        private readonly IBatchRepository batchRepository;
        private readonly IStepRepository stepRepository;

        public StepVariableService(
            IBatchRepository batchRepository,
            IStepRepository stepRepository)
        {
            this.batchRepository = batchRepository;
            this.stepRepository = stepRepository;
        }

        public async Task<CreateStepVariableResponse> Post(CreateStepVariableRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepId);
            if (step == null)
                throw Err.StepNotFound(request.StepId);

            if (await stepRepository.DoesStepVariableExist(request.BatchId, request.StepId, request.VariableId))
                throw Err.StepVariableAlreadyExists(request.VariableId);

            var stepVariable = request.ConvertTo<StepVariable>();
            stepVariable.StepId = step.Id;

            await stepRepository.CreateOrUpdateStepVariable(stepVariable);

            return new CreateStepVariableResponse();
        }

        public async Task<GetStepVariableResponse> Get(GetStepVariableRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepId);
            if (step == null)
                throw Err.StepNotFound(request.StepId);

            var stepVariable = await stepRepository.GetStepVariable(step.Id, request.VariableId);
            if (stepVariable == null)
                throw Err.StepVariableNotFound(request.VariableId);

            return stepVariable.ConvertTo<GetStepVariableResponse>();
        }

        public async Task<GetAllStepVariableResponse> Get(GetAllStepVariableRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepId);
            if (step == null)
                throw Err.StepNotFound(request.StepId);

            var response = new GetAllStepVariableResponse
            {
                Variables = step.Variables.ConvertTo<List<ServiceModels.Variable>>()
            };

            return response;
        }

        public async Task<UpdateStepVariableResponse> Put(UpdateStepVariableRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepId);
            if (step == null)
                throw Err.StepNotFound(request.StepId);

            var existingStepVariable =
                await stepRepository.GetStepVariable(step.Id, request.VariableId);
            if (existingStepVariable == null)
                throw Err.StepVariableNotFound(request.VariableId);

            var stepVariable = request.ConvertTo<StepVariable>();
            stepVariable.Id = existingStepVariable.Id;

            await stepRepository.CreateOrUpdateStepVariable(stepVariable);

            return new UpdateStepVariableResponse();
        }
        
        public async Task<DeleteStepVariableResponse> Delete(DeleteStepVariableRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepId);
            if (step == null)
                throw Err.StepNotFound(request.StepId);

            var existingStepVariable =
                await stepRepository.GetStepVariable(step.Id, request.VariableId);
            if (existingStepVariable == null)
                throw Err.StepVariableNotFound(request.VariableId);

            await stepRepository.DeleteStepVariable(existingStepVariable.Id);

            return new DeleteStepVariableResponse();
        }
    }
}
