using Bakana.ServiceModels;
using Bakana.ServiceModels.Batches;
using Bakana.ServiceModels.Commands;
using Bakana.ServiceModels.Steps;
using ServiceStack;
using Command = Bakana.ServiceModels.Command;
using Option = Bakana.ServiceModels.Option;
using Variable = Bakana.ServiceModels.Variable;

namespace Bakana.TestData
{
    public static class Mappers
    {
        public static void Register()
        {
            AutoMapping.RegisterConverter((Variable from) =>
            {
                var to = from.ConvertTo<CreateBatchVariableRequest>(true);
                to.VariableName = from.Name;

                return to;
            });

            AutoMapping.RegisterConverter((Variable from) =>
            {
                var to = from.ConvertTo<UpdateBatchVariableRequest>(true);
                to.VariableName = from.Name;

                return to;
            });
            
            AutoMapping.RegisterConverter((Variable from) =>
            {
                var to = from.ConvertTo<GetBatchVariableRequest>(true);
                to.VariableName = from.Name;

                return to;
            });

            AutoMapping.RegisterConverter((Variable from) =>
            {
                var to = from.ConvertTo<DeleteBatchVariableRequest>(true);
                to.VariableName = from.Name;

                return to;
            });

            AutoMapping.RegisterConverter((Variable from) =>
            {
                var to = from.ConvertTo<CreateStepVariableRequest>(true);
                to.VariableName = from.Name;

                return to;
            });

            AutoMapping.RegisterConverter((Variable from) =>
            {
                var to = from.ConvertTo<UpdateStepVariableRequest>(true);
                to.VariableName = from.Name;

                return to;
            });
            
            AutoMapping.RegisterConverter((Variable from) =>
            {
                var to = from.ConvertTo<GetStepVariableRequest>(true);
                to.VariableName = from.Name;

                return to;
            });

            AutoMapping.RegisterConverter((Variable from) =>
            {
                var to = from.ConvertTo<DeleteStepVariableRequest>(true);
                to.VariableName = from.Name;

                return to;
            });

            AutoMapping.RegisterConverter((Variable from) =>
            {
                var to = from.ConvertTo<CreateCommandVariableRequest>(true);
                to.VariableName = from.Name;

                return to;
            });

            AutoMapping.RegisterConverter((Variable from) =>
            {
                var to = from.ConvertTo<UpdateCommandVariableRequest>(true);
                to.VariableName = from.Name;

                return to;
            });
            
            AutoMapping.RegisterConverter((Variable from) =>
            {
                var to = from.ConvertTo<GetCommandVariableRequest>(true);
                to.VariableName = from.Name;

                return to;
            });

            AutoMapping.RegisterConverter((Variable from) =>
            {
                var to = from.ConvertTo<DeleteCommandVariableRequest>(true);
                to.VariableName = from.Name;

                return to;
            });
            
            AutoMapping.RegisterConverter((Option from) =>
            {
                var to = from.ConvertTo<CreateBatchOptionRequest>(true);
                to.OptionName = from.Name;

                return to;
            });
            
            AutoMapping.RegisterConverter((Option from) =>
            {
                var to = from.ConvertTo<UpdateBatchOptionRequest>(true);
                to.OptionName = from.Name;

                return to;
            });
            
            AutoMapping.RegisterConverter((Option from) =>
            {
                var to = from.ConvertTo<GetBatchOptionRequest>(true);
                to.OptionName = from.Name;

                return to;
            });
            
            AutoMapping.RegisterConverter((Option from) =>
            {
                var to = from.ConvertTo<DeleteBatchOptionRequest>(true);
                to.OptionName = from.Name;

                return to;
            });

            AutoMapping.RegisterConverter((Option from) =>
            {
                var to = from.ConvertTo<CreateBatchArtifactOptionRequest>(true);
                to.OptionName = from.Name;

                return to;
            });
            
            AutoMapping.RegisterConverter((Option from) =>
            {
                var to = from.ConvertTo<UpdateBatchArtifactOptionRequest>(true);
                to.OptionName = from.Name;

                return to;
            });
            
            AutoMapping.RegisterConverter((Option from) =>
            {
                var to = from.ConvertTo<GetBatchArtifactOptionRequest>(true);
                to.OptionName = from.Name;

                return to;
            });
            
            AutoMapping.RegisterConverter((Option from) =>
            {
                var to = from.ConvertTo<DeleteBatchArtifactOptionRequest>(true);
                to.OptionName = from.Name;

                return to;
            });

            AutoMapping.RegisterConverter((Option from) =>
            {
                var to = from.ConvertTo<CreateStepOptionRequest>(true);
                to.OptionName = from.Name;

                return to;
            });
            
            AutoMapping.RegisterConverter((Option from) =>
            {
                var to = from.ConvertTo<UpdateStepOptionRequest>(true);
                to.OptionName = from.Name;

                return to;
            });
            
            AutoMapping.RegisterConverter((Option from) =>
            {
                var to = from.ConvertTo<GetStepOptionRequest>(true);
                to.OptionName = from.Name;

                return to;
            });
            
            AutoMapping.RegisterConverter((Option from) =>
            {
                var to = from.ConvertTo<DeleteStepOptionRequest>(true);
                to.OptionName = from.Name;

                return to;
            });

            AutoMapping.RegisterConverter((Option from) =>
            {
                var to = from.ConvertTo<CreateStepArtifactOptionRequest>(true);
                to.OptionName = from.Name;

                return to;
            });
            
            AutoMapping.RegisterConverter((Option from) =>
            {
                var to = from.ConvertTo<UpdateStepArtifactOptionRequest>(true);
                to.OptionName = from.Name;

                return to;
            });
            
            AutoMapping.RegisterConverter((Option from) =>
            {
                var to = from.ConvertTo<GetStepArtifactOptionRequest>(true);
                to.OptionName = from.Name;

                return to;
            });
            
            AutoMapping.RegisterConverter((Option from) =>
            {
                var to = from.ConvertTo<DeleteStepArtifactOptionRequest>(true);
                to.OptionName = from.Name;

                return to;
            });

            AutoMapping.RegisterConverter((Option from) =>
            {
                var to = from.ConvertTo<CreateCommandOptionRequest>(true);
                to.OptionName = from.Name;

                return to;
            });
            
            AutoMapping.RegisterConverter((Option from) =>
            {
                var to = from.ConvertTo<UpdateCommandOptionRequest>(true);
                to.OptionName = from.Name;

                return to;
            });
            
            AutoMapping.RegisterConverter((Option from) =>
            {
                var to = from.ConvertTo<GetCommandOptionRequest>(true);
                to.OptionName = from.Name;

                return to;
            });
            
            AutoMapping.RegisterConverter((Option from) =>
            {
                var to = from.ConvertTo<DeleteCommandOptionRequest>(true);
                to.OptionName = from.Name;

                return to;
            });
            
            AutoMapping.RegisterConverter((BatchArtifact from) =>
            {
                var to = from.ConvertTo<CreateBatchArtifactRequest>(true);
                to.ArtifactName = from.Name;

                return to;
            });
            
            AutoMapping.RegisterConverter((BatchArtifact from) =>
            {
                var to = from.ConvertTo<UpdateBatchArtifactRequest>(true);
                to.ArtifactName = from.Name;

                return to;
            });
            
            AutoMapping.RegisterConverter((BatchArtifact from) =>
            {
                var to = from.ConvertTo<GetBatchArtifactRequest>(true);
                to.ArtifactName = from.Name;

                return to;
            });
            
            AutoMapping.RegisterConverter((BatchArtifact from) =>
            {
                var to = from.ConvertTo<DeleteBatchArtifactRequest>(true);
                to.ArtifactName = from.Name;

                return to;
            });
            
            AutoMapping.RegisterConverter((StepArtifact from) =>
            {
                var to = from.ConvertTo<CreateStepArtifactRequest>(true);
                to.ArtifactName = from.Name;

                return to;
            });
            
            AutoMapping.RegisterConverter((StepArtifact from) =>
            {
                var to = from.ConvertTo<UpdateStepArtifactRequest>(true);
                to.ArtifactName = from.Name;

                return to;
            });
            
            AutoMapping.RegisterConverter((StepArtifact from) =>
            {
                var to = from.ConvertTo<GetStepArtifactRequest>(true);
                to.ArtifactName = from.Name;

                return to;
            });
            
            AutoMapping.RegisterConverter((StepArtifact from) =>
            {
                var to = from.ConvertTo<DeleteStepArtifactRequest>(true);
                to.ArtifactName = from.Name;

                return to;
            });
            
            AutoMapping.RegisterConverter((Command from) =>
            {
                var to = from.ConvertTo<CreateCommandRequest>(true);
                to.CommandName = from.Name;

                return to;
            });

            AutoMapping.RegisterConverter((Command from) =>
            {
                var to = from.ConvertTo<UpdateCommandRequest>(true);
                to.CommandName = from.Name;

                return to;
            });
            
            AutoMapping.RegisterConverter((Command from) =>
            {
                var to = from.ConvertTo<GetCommandRequest>(true);
                to.CommandName = from.Name;

                return to;
            });
            
            AutoMapping.RegisterConverter((Command from) =>
            {
                var to = from.ConvertTo<DeleteCommandRequest>(true);
                to.CommandName = from.Name;

                return to;
            });
            
            AutoMapping.RegisterConverter((Step from) =>
            {
                var to = from.ConvertTo<CreateStepRequest>(true);
                to.StepName = from.Name;

                return to;
            });
            
            AutoMapping.RegisterConverter((Step from) =>
            {
                var to = from.ConvertTo<UpdateStepRequest>(true);
                to.StepName = from.Name;

                return to;
            });
            
            AutoMapping.RegisterConverter((Step from) =>
            {
                var to = from.ConvertTo<GetStepRequest>(true);
                to.StepName = from.Name;

                return to;
            });
            
            AutoMapping.RegisterConverter((Step from) =>
            {
                var to = from.ConvertTo<DeleteStepRequest>(true);
                to.StepName = from.Name;

                return to;
            });
        }
    }
}