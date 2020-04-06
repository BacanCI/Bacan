using System.Collections.Generic;
using System.Threading.Tasks;
using Bakana.Core.Entities;
using Bakana.Core.Repositories;
using Bakana.ServiceModels.Commands;
using ServiceStack;

namespace Bakana.ServiceInterface.Commands
{
    public class CommandOptionService : BakanaService
    {
        private readonly IBatchRepository batchRepository;
        private readonly IStepRepository stepRepository;
        private readonly ICommandRepository commandRepository;

        public CommandOptionService(
            IBatchRepository batchRepository,
            IStepRepository stepRepository,
            ICommandRepository commandRepository)
        {
            this.batchRepository = batchRepository;
            this.stepRepository = stepRepository;
            this.commandRepository = commandRepository;
        }

        public async Task<CreateCommandOptionResponse> Post(CreateCommandOptionRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepId);
            if (step == null) throw StepNotFound(request.StepId);

            var command = await commandRepository.Get(step.Id, request.CommandId);
            if (command == null)
                throw CommandNotFound(request.CommandId);

            if (await commandRepository.DoesCommandOptionExist(request.BatchId, request.StepId, request.CommandId, request.OptionId))
                throw CommandOptionAlreadyExists(request.OptionId);

            var commandOption = request.ConvertTo<CommandOption>();
            commandOption.CommandId = command.Id;

            await commandRepository.CreateOrUpdateCommandOption(commandOption);

            return new CreateCommandOptionResponse();
        }

        public async Task<GetCommandOptionResponse> Get(GetCommandOptionRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepId);
            if (step == null) throw StepNotFound(request.StepId);

            var command = await commandRepository.Get(step.Id, request.CommandId);
            if (command == null)
                throw CommandNotFound(request.CommandId);

            var commandOption = await commandRepository.GetCommandOption(command.Id, request.OptionId);
            if (commandOption == null)
                throw CommandOptionNotFound(request.OptionId);

            return commandOption.ConvertTo<GetCommandOptionResponse>();
        }

        public async Task<GetAllCommandOptionResponse> Get(GetAllCommandOptionRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepId);
            if (step == null) throw StepNotFound(request.StepId);

            var command = await commandRepository.Get(step.Id, request.CommandId);
            if (command == null)
                throw CommandNotFound(request.CommandId);

            var response = new GetAllCommandOptionResponse
            {
                Options = command.Options.ConvertTo<List<ServiceModels.Option>>()
            };

            return response;
        }

        public async Task<UpdateCommandOptionResponse> Put(UpdateCommandOptionRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepId);
            if (step == null) throw StepNotFound(request.StepId);

            var command = await commandRepository.Get(step.Id, request.CommandId);
            if (command == null)
                throw CommandNotFound(request.CommandId);

            var existingCommandOption =
                await commandRepository.GetCommandOption(command.Id, request.OptionId);
            if (existingCommandOption == null)
                throw CommandOptionNotFound(request.OptionId);

            var commandOption = request.ConvertTo<CommandOption>();
            commandOption.Id = existingCommandOption.Id;

            await commandRepository.CreateOrUpdateCommandOption(commandOption);

            return new UpdateCommandOptionResponse();
        }
        
        public async Task<DeleteCommandOptionResponse> Delete(DeleteCommandOptionRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepId);
            if (step == null) throw StepNotFound(request.StepId);

            var command = await commandRepository.Get(step.Id, request.CommandId);
            if (command == null)
                throw CommandNotFound(request.CommandId);

            var existingCommandOption =
                await commandRepository.GetCommandOption(command.Id, request.OptionId);
            if (existingCommandOption == null)
                throw CommandOptionNotFound(request.OptionId);

            await commandRepository.DeleteCommandOption(existingCommandOption.Id);

            return new DeleteCommandOptionResponse();
        }
    }
}
