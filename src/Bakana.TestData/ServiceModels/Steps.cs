using System.Collections.Generic;
using Bakana.ServiceModels;
using Bakana.ServiceModels.Steps;
using ServiceStack;
using Command = Bakana.ServiceModels.Command;

namespace Bakana.TestData.ServiceModels
{
    public class StepVariables
    {
        public static Variable SourcePath => new Variable()
        {
            Name = "SourcePath",
            Description = "Path to Source code",
            Value = "./src"
        };

        public static Variable Profile => new Variable
        {
            Name = "Profile",
            Description = "Build Profile ",
            Value = "PRODUCTION"
        };
        
        public static Variable TestPath => new Variable
        {
            Name = "TestPath",
            Description = "Path to test project",
            Value = "./tests"
        };
        
        public static Variable TestFilter => new Variable
        {
            Name = "TestFilter",
            Description = "NUnit Test Filter",
            Value = "--category = 'agency'"
        };
    }
    
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

    public class StepOptions
    {
        public static Option BuildAlways => new Option
        {
            Name = "BuildAlways",
            Description = "Build Always",
            Value = "True"
        };

        public static Option BuildWhenNoErrors => new Option
        {
            Name = "BuildWhenNoErrors",
            Description = "Build only when no previous step has errored",
            Value = "True"
        };
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

    public static class StepArtifactOptions
    {
        public static Option Extract => new Option
        {
            Name = "Extract",
            Description = "Extract files",
            Value = "True"
        };

        public static Option Compress => new Option
        {
            Name = "Compress",
            Description = "Compress files",
            Value = "True"
        };
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

    public static class StepArtifacts
    {
        public static StepArtifact Source => new StepArtifact
        {
            Name = "Source",
            Description = "Source Code",
            FileName = "Source.zip",
            Options = new List<Option>
            {
                StepArtifactOptions.Extract
            },
        };

        public static StepArtifact Binaries => new StepArtifact
        {
            Name = "Binaries",
            Description = "Binaries",
            FileName = "Build.zip",
            OutputArtifact = true,
            Options = new List<Option>
            {
                StepArtifactOptions.Compress
            },
        };

        public static StepArtifact TestResults => new StepArtifact
        {
            Name = "Results",
            Description = "Test Results",
            FileName = "Results.zip",
            OutputArtifact = true,
        };
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

    public static class Steps
    {
        public static Step Build => new Step
        {
            Name = "BuildStep",
            Description = "Build Step",
            Tags = new[] {"Build"},
            Requirements = new[] {"Docker", "Build"},
            Artifacts = new List<StepArtifact>
            {
                StepArtifacts.Source,
                StepArtifacts.Binaries
            },
            Options = new List<Option>
            {
                StepOptions.BuildAlways,
            },
            Variables = new List<Variable>
            {
                StepVariables.SourcePath,
                StepVariables.Profile,
            },
            Commands = new List<Command>
            {
                Commands.DotNetRestore,
                Commands.DotNetBuild,
            },
        };

        public static Step Test => new Step
        {
            Name = "TestStep",
            Description = "Test Step",
            Dependencies = new[] {"BuildStep"},
            Tags = new[] {"TEST"},
            Requirements = new[] {"Windows","Database"},
            Artifacts = new List<StepArtifact>
            {
                StepArtifacts.Source,
                StepArtifacts.TestResults
            },
            Options = new List<Option>
            {
                StepOptions.BuildAlways
            },
            Variables = new List<Variable>
            {
                StepVariables.TestPath,
                StepVariables.TestFilter
            },
            Commands = new List<Command>
            {
                Commands.Deploy,
                Commands.Test,
            },
        };
    }

    public static class CreateSteps
    {
        public static CreateStepRequest Build = Steps.Build.ConvertTo<CreateStepRequest>();
        
        public static CreateStepRequest Test = Steps.Test.ConvertTo<CreateStepRequest>();
    }
    
    public static class UpdateSteps
    {
        public static UpdateStepRequest Build = Steps.Build.ConvertTo<UpdateStepRequest>();
        
        public static UpdateStepRequest Test = Steps.Test.ConvertTo<UpdateStepRequest>();
    }
}