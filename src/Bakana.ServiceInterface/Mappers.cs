using System.Collections.Generic;
using Bakana.Core.Entities;
using Bakana.ServiceModels.Batches;
using ServiceStack;
using BatchArtifact = Bakana.Core.Entities.BatchArtifact;
using Command = Bakana.Core.Entities.Command;
using Step = Bakana.Core.Entities.Step;
using StepArtifact = Bakana.Core.Entities.StepArtifact;

namespace Bakana.ServiceInterface
{
    public static class Mappers
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
            
            
            
            AutoMapping.RegisterConverter((Batch from) =>
            {
                var to = from.ConvertTo<GetBatchResponse>(true);
                to.BatchId = from.Id;
                to.Options = from.Options.ConvertTo<List<ServiceModels.Option>>(true);
                to.Variables = from.Variables.ConvertTo<List<ServiceModels.Variable>>(true);
                to.InputArtifacts = from.Artifacts.ConvertTo<List<ServiceModels.BatchArtifact>>(true);
                to.Steps = from.Steps.ConvertTo<List<ServiceModels.Step>>(true);

                return to;
            });

            AutoMapping.RegisterConverter((BatchArtifact from) =>
            {
                var to = from.ConvertTo<ServiceModels.BatchArtifact>(true);
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
            
            AutoMapping.RegisterConverter((Command from) =>
            {
                var to = from.ConvertTo<ServiceModels.Command>(true);
                to.Options = from.Options.ConvertTo<List<ServiceModels.Option>>(true);
                to.Variables = from.Variables.ConvertTo<List<ServiceModels.Variable>>(true);

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

                return to;
            });
            
            AutoMapping.RegisterConverter((UpdateBatchVariableRequest from) =>
            {
                var to = from.ConvertTo<BatchVariable>(true);

                return to;
            });
            
            AutoMapping.RegisterConverter((BatchVariable from) =>
            {
                var to = from.ConvertTo<GetBatchVariableResponse>(true);

                return to;
            });
            
            
            
            AutoMapping.RegisterConverter((CreateBatchOptionRequest from) =>
            {
                var to = from.ConvertTo<BatchOption>(true);

                return to;
            });
            
            AutoMapping.RegisterConverter((UpdateBatchOptionRequest from) =>
            {
                var to = from.ConvertTo<BatchOption>(true);

                return to;
            });
            
            AutoMapping.RegisterConverter((BatchOption from) =>
            {
                var to = from.ConvertTo<GetBatchOptionResponse>(true);

                return to;
            });
        }
        
    }
}