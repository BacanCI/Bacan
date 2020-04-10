using System.Collections.Generic;
using System.Threading.Tasks;
using Bakana.Core.Entities;
using Bakana.Core.Repositories;
using Bakana.ServiceModels.Commands;
using ServiceStack;

namespace Bakana.ServiceInterface.Commands
{
    public class CommandOptionService : Service
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
                throw Err.BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepName);
            if (step == null) throw Err.StepNotFound(request.StepName);

            var command = await commandRepository.Get(step.Id, request.CommandName);
            if (command == null)
                throw Err.CommandNotFound(request.CommandName);

            if (await commandRepository.DoesCommandOptionExist(request.BatchId, request.StepName, request.CommandName, request.OptionName))
                throw Err.CommandOptionAlreadyExists(request.OptionName);

            var commandOption = request.ConvertTo<CommandOption>();
            commandOption.CommandId = command.Id;

            await commandRepository.CreateOrUpdateCommandOption(commandOption);

            return new CreateCommandOptionResponse();
        }

        public async Task<GetCommandOptionResponse> Get(GetCommandOptionRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepName);
            if (step == null) throw Err.StepNotFound(request.StepName);

            var command = await commandRepository.Get(step.Id, request.CommandName);
            if (command == null)
                throw Err.CommandNotFound(request.CommandName);

            var commandOption = await commandRepository.GetCommandOption(command.Id, request.OptionName);
            if (commandOption == null)
                throw Err.CommandOptionNotFound(request.OptionName);

            return commandOption.ConvertTo<GetCommandOptionResponse>();
        }

        public async Task<GetAllCommandOptionResponse> Get(GetAllCommandOptionRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepName);
            if (step == null) throw Err.StepNotFound(request.StepName);

            var command = await commandRepository.Get(step.Id, request.CommandName);
            if (command == null)
                throw Err.CommandNotFound(request.CommandName);

            var response = new GetAllCommandOptionResponse
            {
                Options = command.Options.ConvertTo<List<ServiceModels.Option>>()
            };

            return response;
        }

        public async Task<UpdateCommandOptionResponse> Put(UpdateCommandOptionRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepName);
            if (step == null) throw Err.StepNotFound(request.StepName);

            var command = await commandRepository.Get(step.Id, request.CommandName);
            if (command == null)
                throw Err.CommandNotFound(request.CommandName);

            var existingCommandOption =
                await commandRepository.GetCommandOption(command.Id, request.OptionName);
            if (existingCommandOption == null)
                throw Err.CommandOptionNotFound(request.OptionName);

            var commandOption = request.ConvertTo<CommandOption>();
            commandOption.Id = existingCommandOption.Id;
            commandOption.CommandId = command.Id;

            await commandRepository.CreateOrUpdateCommandOption(commandOption);

            return new UpdateCommandOptionResponse();
        }
        
        public async Task<DeleteCommandOptionResponse> Delete(DeleteCommandOptionRequest request)
        {
            if (!await batchRepository.DoesBatchExist(request.BatchId)) 
                throw Err.BatchNotFound(request.BatchId);

            var step = await stepRepository.Get(request.BatchId, request.StepName);
            if (step == null) throw Err.StepNotFound(request.StepName);

            var command = await commandRepository.Get(step.Id, request.CommandName);
            if (command == null)
                throw Err.CommandNotFound(request.CommandName);

            var existingCommandOption =
                await commandRepository.GetCommandOption(command.Id, request.OptionName);
            if (existingCommandOption == null)
                throw Err.CommandOptionNotFound(request.OptionName);

            await commandRepository.DeleteCommandOption(existingCommandOption.Id);

            return new DeleteCommandOptionResponse();
        }
    }
}
