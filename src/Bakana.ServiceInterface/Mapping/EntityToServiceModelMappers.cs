using System.Collections.Generic;
using Bakana.Core.Entities;
using Bakana.ServiceModels.Batches;
using Bakana.ServiceModels.Commands;
using Bakana.ServiceModels.Steps;
using ServiceStack;
using Command = Bakana.Core.Entities.Command;

namespace Bakana.ServiceInterface.Mapping
{
    public static class EntityToServiceModelMappers
    {
        public static void Register()
        {
            AutoMapping.RegisterConverter((Batch from) =>
            {
                var to = from.ConvertTo<GetBatchResponse>(true);
                to.BatchId = from.Id;
                to.Options = from.Options.ConvertTo<List<ServiceModels.Option>>(true);
                to.Variables = from.Variables.ConvertTo<List<ServiceModels.Variable>>(true);
                to.Artifacts = from.Artifacts.ConvertTo<List<ServiceModels.BatchArtifact>>(true);
                to.Steps = from.Steps.ConvertTo<List<ServiceModels.Step>>(true);

                return to;
            });

            AutoMapping.RegisterConverter((BatchArtifact from) =>
            {
                var to = from.ConvertTo<ServiceModels.BatchArtifact>(true);
                to.Options = from.Options.ConvertTo<List<ServiceModels.Option>>(true);

                return to;
            });

            AutoMapping.RegisterConverter((BatchArtifact from) =>
            {
                var to = from.ConvertTo<GetBatchArtifactResponse>(true);
                to.ArtifactName = from.Name;
                to.Options = from.Options.ConvertTo<List<ServiceModels.Option>>(true);

                return to;
            });

            AutoMapping.RegisterConverter((Step from) =>
            {
                var to = from.ConvertTo<ServiceModels.Step>(true);
                to.Options = from.Options.ConvertTo<List<ServiceModels.Option>>(true);
                to.Variables = from.Variables.ConvertTo<List<ServiceModels.Variable>>(true);
                to.Artifacts = from.Artifacts.ConvertTo<List<ServiceModels.StepArtifact>>(true);
                to.Commands = from.Commands.ConvertTo<List<ServiceModels.Command>>(true);

                return to;
            });
            
            AutoMapping.RegisterConverter((Step from) =>
            {
                var to = from.ConvertTo<GetStepResponse>(true);
                to.StepName = from.Name;
                to.Options = from.Options.ConvertTo<List<ServiceModels.Option>>(true);
                to.Variables = from.Variables.ConvertTo<List<ServiceModels.Variable>>(true);
                to.Artifacts = from.Artifacts.ConvertTo<List<ServiceModels.StepArtifact>>(true);
                to.Commands = from.Commands.ConvertTo<List<ServiceModels.Command>>(true);

                return to;
            });

            AutoMapping.RegisterConverter((Command from) =>
            {
                var to = from.ConvertTo<ServiceModels.Command>(true);
                to.Options = from.Options.ConvertTo<List<ServiceModels.Option>>(true);
                to.Variables = from.Variables.ConvertTo<List<ServiceModels.Variable>>(true);

                return to;
            });

            AutoMapping.RegisterConverter((Command from) =>
            {
                var to = from.ConvertTo<GetCommandResponse>(true);
                to.CommandName = from.Name;
                to.Options = from.Options.ConvertTo<List<ServiceModels.Option>>(true);
                to.Variables = from.Variables.ConvertTo<List<ServiceModels.Variable>>(true);

                return to;
            });

            AutoMapping.RegisterConverter((StepArtifact from) =>
            {
                var to = from.ConvertTo<GetStepArtifactResponse>(true);
                to.ArtifactName = from.Name;
                to.Options = from.Options.ConvertTo<List<ServiceModels.Option>>(true);

                return to;
            });

            AutoMapping.RegisterConverter((BatchVariable from) =>
            {
                var to = from.ConvertTo<GetBatchVariableResponse>(true);
                to.VariableName = from.Name;

                return to;
            });

            AutoMapping.RegisterConverter((StepVariable from) =>
            {
                var to = from.ConvertTo<GetStepVariableResponse>(true);
                to.VariableName = from.Name;

                return to;
            });

            AutoMapping.RegisterConverter((CommandVariable from) =>
            {
                var to = from.ConvertTo<GetCommandVariableResponse>(true);
                to.VariableName = from.Name;

                return to;
            });

            AutoMapping.RegisterConverter((BatchOption from) =>
            {
                var to = from.ConvertTo<GetBatchOptionResponse>(true);
                to.OptionName = from.Name;

                return to;
            });

            AutoMapping.RegisterConverter((BatchArtifactOption from) =>
            {
                var to = from.ConvertTo<GetBatchArtifactOptionResponse>(true);
                to.OptionName = from.Name;

                return to;
            });

            AutoMapping.RegisterConverter((StepOption from) =>
            {
                var to = from.ConvertTo<GetStepOptionResponse>(true);
                to.OptionName = from.Name;

                return to;
            });
            
            AutoMapping.RegisterConverter((StepArtifactOption from) =>
            {
                var to = from.ConvertTo<GetStepArtifactOptionResponse>(true);
                to.OptionName = from.Name;

                return to;
            });
            
            AutoMapping.RegisterConverter((CommandOption from) =>
            {
                var to = from.ConvertTo<GetCommandOptionResponse>(true);
                to.OptionName = from.Name;

                return to;
            });
        }
    }
}