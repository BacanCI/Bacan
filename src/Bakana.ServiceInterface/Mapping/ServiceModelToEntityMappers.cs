using System.Collections.Generic;
using Bakana.Core.Entities;
using Bakana.ServiceModels.Batches;
using Bakana.ServiceModels.Commands;
using Bakana.ServiceModels.Steps;
using ServiceStack;
using BatchArtifact = Bakana.Core.Entities.BatchArtifact;
using Command = Bakana.Core.Entities.Command;
using Step = Bakana.Core.Entities.Step;
using StepArtifact = Bakana.Core.Entities.StepArtifact;

namespace Bakana.ServiceInterface.Mapping
{
    public static class ServiceModelToEntityMappers
    {
        public static void Register()
        {
            AutoMapping.RegisterConverter((CreateBatchRequest from) =>
            {
                var to = from.ConvertTo<Batch>(true);
                to.Options = from.Options.ConvertTo<List<BatchOption>>(true);
                to.Variables = from.Variables.ConvertTo<List<BatchVariable>>(true);
                to.Artifacts = from.Artifacts.ConvertTo<List<BatchArtifact>>(true);
                to.Steps = from.Steps.ConvertTo<List<Step>>(true);

                return to;
            });
            
            AutoMapping.RegisterConverter((Bakana.ServiceModels.BatchArtifact from) =>
            {
                var to = from.ConvertTo<BatchArtifact>(true);
                to.Options = from.Options.ConvertTo<List<BatchArtifactOption>>(true);

                return to;
            });
            
            AutoMapping.RegisterConverter((Bakana.ServiceModels.Step from) =>
            {
                var to = from.ConvertTo<Step>(true);
                to.Options = from.Options.ConvertTo<List<StepOption>>(true);
                to.Variables = from.Variables.ConvertTo<List<StepVariable>>(true);
                to.Artifacts = from.Artifacts.ConvertTo<List<StepArtifact>>(true);
                to.Commands = from.Commands.ConvertTo<List<Command>>(true);

                return to;
            });
            
            AutoMapping.RegisterConverter((Bakana.ServiceModels.Command from) =>
            {
                var to = from.ConvertTo<Command>(true);
                to.Options = from.Options.ConvertTo<List<CommandOption>>(true);
                to.Variables = from.Variables.ConvertTo<List<CommandVariable>>(true);

                return to;
            });

            AutoMapping.RegisterConverter((UpdateBatchRequest from) =>
            {
                var to = from.ConvertTo<Batch>(true);
                to.Id = from.BatchId;

                return to;
            });
            
            AutoMapping.RegisterConverter((CreateBatchVariableRequest from) =>
            {
                var to = from.ConvertTo<BatchVariable>(true);
                to.Name = from.VariableName;

                return to;
            });
            
            AutoMapping.RegisterConverter((UpdateBatchVariableRequest from) =>
            {
                var to = from.ConvertTo<BatchVariable>(true);
                to.Name = from.VariableName;

                return to;
            });

            AutoMapping.RegisterConverter((CreateStepVariableRequest from) =>
            {
                var to = from.ConvertTo<StepVariable>(true);
                to.Name = from.VariableName;

                return to;
            });

            AutoMapping.RegisterConverter((UpdateStepVariableRequest from) =>
            {
                var to = from.ConvertTo<StepVariable>(true);
                to.Name = from.VariableName;

                return to;
            });
            
            AutoMapping.RegisterConverter((CreateCommandVariableRequest from) =>
            {
                var to = from.ConvertTo<CommandVariable>(true);
                to.Name = from.VariableName;

                return to;
            });
            
            AutoMapping.RegisterConverter((UpdateCommandVariableRequest from) =>
            {
                var to = from.ConvertTo<CommandVariable>(true);
                to.Name = from.VariableName;

                return to;
            });
            
            
            AutoMapping.RegisterConverter((CreateBatchOptionRequest from) =>
            {
                var to = from.ConvertTo<BatchOption>(true);
                to.Name = from.OptionName;

                return to;
            });
            
            AutoMapping.RegisterConverter((UpdateBatchOptionRequest from) =>
            {
                var to = from.ConvertTo<BatchOption>(true);
                to.Name = from.OptionName;

                return to;
            });

            AutoMapping.RegisterConverter((CreateBatchArtifactOptionRequest from) =>
            {
                var to = from.ConvertTo<BatchArtifactOption>(true);
                to.Name = from.OptionName;

                return to;
            });

            AutoMapping.RegisterConverter((UpdateBatchArtifactOptionRequest from) =>
            {
                var to = from.ConvertTo<BatchArtifactOption>(true);
                to.Name = from.OptionName;

                return to;
            });

            AutoMapping.RegisterConverter((CreateStepOptionRequest from) =>
            {
                var to = from.ConvertTo<StepOption>(true);
                to.Name = from.OptionName;

                return to;
            });

            AutoMapping.RegisterConverter((UpdateStepOptionRequest from) =>
            {
                var to = from.ConvertTo<StepOption>(true);
                to.Name = from.OptionName;

                return to;
            });

            AutoMapping.RegisterConverter((CreateStepArtifactOptionRequest from) =>
            {
                var to = from.ConvertTo<StepArtifactOption>(true);
                to.Name = from.OptionName;

                return to;
            });

            AutoMapping.RegisterConverter((UpdateStepArtifactOptionRequest from) =>
            {
                var to = from.ConvertTo<StepArtifactOption>(true);
                to.Name = from.OptionName;

                return to;
            });

            AutoMapping.RegisterConverter((CreateCommandOptionRequest from) =>
            {
                var to = from.ConvertTo<CommandOption>(true);
                to.Name = from.OptionName;

                return to;
            });
            
            AutoMapping.RegisterConverter((UpdateCommandOptionRequest from) =>
            {
                var to = from.ConvertTo<CommandOption>(true);
                to.Name = from.OptionName;

                return to;
            });
            
            AutoMapping.RegisterConverter((CreateBatchArtifactRequest from) =>
            {
                var to = from.ConvertTo<BatchArtifact>(true);
                to.Name = from.ArtifactName;

                return to;
            });

            AutoMapping.RegisterConverter((UpdateBatchArtifactRequest from) =>
            {
                var to = from.ConvertTo<BatchArtifact>(true);
                to.Name = from.ArtifactName;

                return to;
            });
            
            AutoMapping.RegisterConverter((CreateStepArtifactRequest from) =>
            {
                var to = from.ConvertTo<StepArtifact>(true);
                to.Name = from.ArtifactName;

                return to;
            });
            
            AutoMapping.RegisterConverter((UpdateStepArtifactRequest from) =>
            {
                var to = from.ConvertTo<StepArtifact>(true);
                to.Name = from.ArtifactName;

                return to;
            });
            
            AutoMapping.RegisterConverter((CreateStepRequest from) =>
            {
                var to = from.ConvertTo<Step>(true);
                to.Name = from.StepName;

                return to;
            });
            
            AutoMapping.RegisterConverter((UpdateStepRequest from) =>
            {
                var to = from.ConvertTo<Step>(true);
                to.Name = from.StepName;

                return to;
            });
            
            AutoMapping.RegisterConverter((CreateCommandRequest from) =>
            {
                var to = from.ConvertTo<Command>(true);
                to.Name = from.CommandName;

                return to;
            });
            
            AutoMapping.RegisterConverter((UpdateCommandRequest from) =>
            {
                var to = from.ConvertTo<Command>(true);
                to.Name = from.CommandName;

                return to;
            });
        }
    }
}