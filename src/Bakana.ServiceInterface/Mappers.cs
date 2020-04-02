using System.Collections.Generic;
using Bakana.Core.Entities;
using Bakana.ServiceModels;
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
                var to = from.ConvertTo<Batch>();
                to.Options = from.Options.ConvertTo<List<BatchOption>>();
                to.Variables = from.Variables.ConvertTo<List<BatchVariable>>();
                to.InputArtifacts = from.InputArtifacts.ConvertTo<List<BatchArtifact>>();
                to.Steps = from.Steps.ConvertTo<List<Step>>();

                return to;
            });
            
            AutoMapping.RegisterConverter((Bakana.ServiceModels.BatchArtifact from) =>
            {
                var to = from.ConvertTo<BatchArtifact>();
                to.Options = from.Options.ConvertTo<List<BatchArtifactOption>>();

                return to;
            });
            
            AutoMapping.RegisterConverter((Bakana.ServiceModels.Step from) =>
            {
                var to = from.ConvertTo<Step>();
                to.Options = from.Options.ConvertTo<List<StepOption>>();
                to.Variables = from.Variables.ConvertTo<List<StepVariable>>();
                to.InputArtifacts = from.InputArtifacts.ConvertTo<List<StepArtifact>>();
                to.OutputArtifacts = from.OutputArtifacts.ConvertTo<List<StepArtifact>>();
                to.Commands = from.Commands.ConvertTo<List<Command>>();

                return to;
            });
            
            AutoMapping.RegisterConverter((Bakana.ServiceModels.Command from) =>
            {
                var to = from.ConvertTo<Command>();
                to.Options = from.Options.ConvertTo<List<CommandOption>>();
                to.Variables = from.Variables.ConvertTo<List<CommandVariable>>();

                return to;
            });
        }
        
    }
}