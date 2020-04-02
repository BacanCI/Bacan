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
                var to = from.ConvertTo<Batch>(true);
                to.Options = from.Options.ConvertTo<List<BatchOption>>(true);
                to.Variables = from.Variables.ConvertTo<List<BatchVariable>>(true);
                to.InputArtifacts = from.InputArtifacts.ConvertTo<List<BatchArtifact>>(true);
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
                to.InputArtifacts = from.InputArtifacts.ConvertTo<List<StepArtifact>>(true);
                to.OutputArtifacts = from.OutputArtifacts.ConvertTo<List<StepArtifact>>(true);
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
        }
        
    }
}