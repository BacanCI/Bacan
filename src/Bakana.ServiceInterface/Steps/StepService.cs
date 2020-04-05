using System.Threading.Tasks;
using Bakana.Core.Entities;
using Bakana.Core.Repositories;
using Bakana.ServiceModels.Steps;
using ServiceStack;

namespace Bakana.ServiceInterface.Steps
{
    public class StepService : BakanaService
    {
        private readonly IBatchRepository batchRepository;
        private readonly IStepRepository stepRepository;

        public StepService(
            IBatchRepository batchRepository,
            IStepRepository stepRepository)
        {
            this.batchRepository = batchRepository;
            this.stepRepository = stepRepository;
        }
        
        public async Task<CreateStepResponse> Post(CreateStepRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw BatchNotFound(request.BatchId);

            if (await stepRepository.DoesStepExist(request.BatchId, request.StepId))
                throw StepArtifactAlreadyExists(request.StepId);

            var step = request.ConvertTo<Step>();

            await stepRepository.Create(step);

            return new CreateStepResponse();
        }

        public async Task<GetStepResponse> Get(GetStepRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepId);
            if (step == null) throw StepNotFound(request.StepId);

            return step.ConvertTo<GetStepResponse>();
        }
        
        public async Task<UpdateStepResponse> Put(UpdateStepRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw BatchNotFound(request.BatchId);

            var step = request.ConvertTo<Step>();

            var updated = await stepRepository.Update(step);
            if (!updated) throw StepNotFound(request.StepId);

            return new UpdateStepResponse();
        }
        
        public async Task<DeleteStepResponse> Delete(DeleteStepRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepId);
            if (step == null) throw StepNotFound(request.StepId);

            await stepRepository.Delete(step.Id);

            return new DeleteStepResponse();
        }
    }
}
