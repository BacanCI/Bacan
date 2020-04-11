using Bakana.ServiceModels.Steps;
using Bakana.TestData.DomainModels;
using ServiceStack;

namespace Bakana.TestData.ServiceModels
{
    public static class CreateStepVariables
    {
        public static CreateStepVariableRequest SourcePath =
            StepVariables.SourcePath.ConvertTo<CreateStepVariableRequest>();
        public static CreateStepVariableRequest Profile =
            StepVariables.Profile.ConvertTo<CreateStepVariableRequest>();
        public static CreateStepVariableRequest TestPath =
            StepVariables.TestPath.ConvertTo<CreateStepVariableRequest>();
        public static CreateStepVariableRequest TestFilter =
            StepVariables.TestFilter.ConvertTo<CreateStepVariableRequest>();
    }

    public static class UpdateStepVariables
    {
        public static UpdateStepVariableRequest SourcePath =
            StepVariables.SourcePath.ConvertTo<UpdateStepVariableRequest>();
        public static UpdateStepVariableRequest Profile =
            StepVariables.Profile.ConvertTo<UpdateStepVariableRequest>();
        public static UpdateStepVariableRequest TestPath =
            StepVariables.TestPath.ConvertTo<UpdateStepVariableRequest>();
        public static UpdateStepVariableRequest TestFilter =
            StepVariables.TestFilter.ConvertTo<UpdateStepVariableRequest>();
    }

    public static class CreateStepOptions
    {
        public static CreateStepOptionRequest
            BuildAlways = StepOptions.BuildAlways.ConvertTo<CreateStepOptionRequest>();
        public static CreateStepOptionRequest
            BuildWhenNoErrors = StepOptions.BuildWhenNoErrors.ConvertTo<CreateStepOptionRequest>();
    }

    public static class UpdateStepOptions
    {
        public static UpdateStepOptionRequest
            BuildAlways = StepOptions.BuildAlways.ConvertTo<UpdateStepOptionRequest>();
        public static UpdateStepOptionRequest
            BuildWhenNoErrors = StepOptions.BuildWhenNoErrors.ConvertTo<UpdateStepOptionRequest>();
    }

    public static class CreateStepArtifactOptions
    {
        public static CreateStepArtifactOptionRequest Extract =
            StepArtifactOptions.Extract.ConvertTo<CreateStepArtifactOptionRequest>();
        public static CreateStepArtifactOptionRequest Compress =
            StepArtifactOptions.Compress.ConvertTo<CreateStepArtifactOptionRequest>();
    }

    public static class UpdateStepArtifactOptions
    {
        public static UpdateStepArtifactOptionRequest Extract =
            StepArtifactOptions.Extract.ConvertTo<UpdateStepArtifactOptionRequest>();
        public static UpdateStepArtifactOptionRequest Compress =
            StepArtifactOptions.Compress.ConvertTo<UpdateStepArtifactOptionRequest>();
    }

    public static class CreateStepArtifacts
    {
        public static CreateStepArtifactRequest Source = StepArtifacts.Source.ConvertTo<CreateStepArtifactRequest>();
        public static CreateStepArtifactRequest Binaries = StepArtifacts.Binaries.ConvertTo<CreateStepArtifactRequest>();
        public static CreateStepArtifactRequest TestResults = StepArtifacts.TestResults.ConvertTo<CreateStepArtifactRequest>();
    }

    public static class UpdateStepArtifacts
    {
        public static UpdateStepArtifactRequest Source = StepArtifacts.Source.ConvertTo<UpdateStepArtifactRequest>();
        public static UpdateStepArtifactRequest Binaries = StepArtifacts.Binaries.ConvertTo<UpdateStepArtifactRequest>();
        public static UpdateStepArtifactRequest TestResults = StepArtifacts.TestResults.ConvertTo<UpdateStepArtifactRequest>();
    }

    public static class CreateSteps
    {
        public static CreateStepRequest Build = TestData.DomainModels.Steps.Build.ConvertTo<CreateStepRequest>();
    
        public static CreateStepRequest Test = TestData.DomainModels.Steps.Test.ConvertTo<CreateStepRequest>();
    }

    public static class UpdateSteps
    {
        public static UpdateStepRequest Build = TestData.DomainModels.Steps.Build.ConvertTo<UpdateStepRequest>();
    
        public static UpdateStepRequest Test = TestData.DomainModels.Steps.Test.ConvertTo<UpdateStepRequest>();
    }
}