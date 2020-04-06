using System.Collections.Generic;
using System.Threading.Tasks;
using Bakana.Core.Entities;
using Bakana.Core.Repositories;
using Bakana.ServiceModels.Commands;
using ServiceStack;

namespace Bakana.ServiceInterface.Commands
{
    public class CommandVariableService : Service
    {
        private readonly IBatchRepository batchRepository;
        private readonly IStepRepository stepRepository;
        private readonly ICommandRepository commandRepository;

        public CommandVariableService(
            IBatchRepository batchRepository,
            IStepRepository stepRepository,
            ICommandRepository commandRepository)
        {
            this.batchRepository = batchRepository;
            this.stepRepository = stepRepository;
            this.commandRepository = commandRepository;
        }

        public async Task<CreateCommandVariableResponse> Post(CreateCommandVariableRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepId);
            if (step == null) throw Err.StepNotFound(request.StepId);

            var command = await commandRepository.Get(step.Id, request.CommandId);
            if (command == null)
                throw Err.CommandNotFound(request.CommandId);

            if (await commandRepository.DoesCommandVariableExist(request.BatchId, request.StepId, request.CommandId, request.VariableId))
                throw Err.CommandVariableAlreadyExists(request.VariableId);

            var commandVariable = request.ConvertTo<CommandVariable>();
            commandVariable.CommandId = command.Id;

            await commandRepository.CreateOrUpdateCommandVariable(commandVariable);

            return new CreateCommandVariableResponse();
        }

        public async Task<GetCommandVariableResponse> Get(GetCommandVariableRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepId);
            if (step == null) throw Err.StepNotFound(request.StepId);

            var command = await commandRepository.Get(step.Id, request.CommandId);
            if (command == null)
                throw Err.CommandNotFound(request.CommandId);

            var commandVariable = await commandRepository.GetCommandVariable(command.Id, request.VariableId);
            if (commandVariable == null)
                throw Err.CommandVariableNotFound(request.VariableId);

            return commandVariable.ConvertTo<GetCommandVariableResponse>();
        }

        public async Task<GetAllCommandVariableResponse> Get(GetAllCommandVariableRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepId);
            if (step == null) throw Err.StepNotFound(request.StepId);

            var command = await commandRepository.Get(step.Id, request.CommandId);
            if (command == null)
                throw Err.CommandNotFound(request.CommandId);

            var response = new GetAllCommandVariableResponse
            {
                Variables = command.Variables.ConvertTo<List<ServiceModels.Variable>>()
            };

            return response;
        }

        public async Task<UpdateCommandVariableResponse> Put(UpdateCommandVariableRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepId);
            if (step == null) throw Err.StepNotFound(request.StepId);

            var command = await commandRepository.Get(step.Id, request.CommandId);
            if (command == null)
                throw Err.CommandNotFound(request.CommandId);

            var existingCommandVariable =
                await commandRepository.GetCommandVariable(command.Id, request.VariableId);
            if (existingCommandVariable == null)
                throw Err.CommandVariableNotFound(request.VariableId);

            var commandVariable = request.ConvertTo<CommandVariable>();
            commandVariable.Id = existingCommandVariable.Id;

            await commandRepository.CreateOrUpdateCommandVariable(commandVariable);

            return new UpdateCommandVariableResponse();
        }
        
        public async Task<DeleteCommandVariableResponse> Delete(DeleteCommandVariableRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepId);
            if (step == null) throw Err.StepNotFound(request.StepId);

            var command = await commandRepository.Get(step.Id, request.CommandId);
            if (command == null)
                throw Err.CommandNotFound(request.CommandId);

            var existingCommandVariable =
                await commandRepository.GetCommandVariable(command.Id, request.VariableId);
            if (existingCommandVariable == null)
                throw Err.CommandVariableNotFound(request.VariableId);

            await commandRepository.DeleteCommandVariable(existingCommandVariable.Id);

            return new DeleteCommandVariableResponse();
        }
    }
}
