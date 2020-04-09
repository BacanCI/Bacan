using System.Collections.Generic;
using System.Threading.Tasks;
using Bakana.Core.Repositories;
using Bakana.ServiceModels.Commands;
using ServiceStack;

namespace Bakana.ServiceInterface.Commands
{
    public class CommandService : Service
    {
        private readonly IBatchRepository batchRepository;
        private readonly IStepRepository stepRepository;
        private readonly ICommandRepository commandRepository;

        public CommandService(
            IBatchRepository batchRepository,
            IStepRepository stepRepository,
            ICommandRepository commandRepository)
        {
            this.batchRepository = batchRepository;
            this.stepRepository = stepRepository;
            this.commandRepository = commandRepository;
        }
        
        public async Task<CreateCommandResponse> Post(CreateCommandRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepId);
            if (step == null) throw Err.StepNotFound(request.StepId);

            if (await commandRepository.DoesCommandExist(request.BatchId, request.StepId, request.CommandId))
                throw Err.CommandAlreadyExists(request.CommandId);

            var command = request.ConvertTo<Core.Entities.Command>();
            command.StepId = step.Id;

            await commandRepository.Create(command);

            return new CreateCommandResponse();
        }

        public async Task<GetCommandResponse> Get(GetCommandRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepId);
            if (step == null) throw Err.StepNotFound(request.StepId);

            var command = await commandRepository.Get(step.Id, request.CommandId);
            if (command == null) throw Err.CommandNotFound(request.CommandId);

            return command.ConvertTo<GetCommandResponse>();
        }
        
        public async Task<GetAllCommandsResponse> Get(GetAllCommandsRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepId);
            if (step == null) throw Err.StepNotFound(request.StepId);

            return new GetAllCommandsResponse
            {
                Commands = step.Commands.ConvertTo<List<ServiceModels.Command>>()
            };
        }

        public async Task<UpdateCommandResponse> Put(UpdateCommandRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepId);
            if (step == null) throw Err.StepNotFound(request.StepId);

            var command = request.ConvertTo<Core.Entities.Command>();
            command.StepId = step.Id;

            var updated = await commandRepository.Update(command);
            if (!updated) throw Err.CommandNotFound(request.CommandId);

            return new UpdateCommandResponse();
        }
        
        public async Task<DeleteCommandResponse> Delete(DeleteCommandRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepId);
            if (step == null) throw Err.StepNotFound(request.StepId);

            var command = await commandRepository.Get(step.Id, request.CommandId);
            if (command == null) throw Err.CommandNotFound(request.CommandId);

            await commandRepository.Delete(command.Id);

            return new DeleteCommandResponse();
        }
    }
}
