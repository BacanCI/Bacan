using System.Collections.Generic;
using Bakana.ServiceModels;

namespace Bakana.UnitTests.TestData
{
    public class StepVariables
    {
        public static Variable SourcePath => new Variable()
        {
            VariableId = "SourcePath",
            Description = "Path to Source code",
            Value = "./src"
        };

        public static Variable Profile => new Variable
        {
            VariableId = "Profile",
            Description = "Build Profile ",
            Value = "PRODUCTION"
        };
        
        public static Variable TestPath => new Variable
        {
            VariableId = "TestPath",
            Description = "Path to test project",
            Value = "./tests"
        };
        
        public static Variable TestFilter => new Variable
        {
            VariableId = "TestFilter",
            Description = "NUnit Test Filter",
            Value = "--category = 'agency'"
        };
    }
    
    public class StepOptions
    {
        public static Option BuildAlways => new Option
        {
            OptionId = "BuildAlways",
            Description = "Build Always",
            Value = "True"
        };

        public static Option BuildWhenNoErrors => new Option
        {
            OptionId = "BuildWhenNoErrors",
            Description = "Build only when no previous step has errored",
            Value = "True"
        };
    }
    
    public static class StepArtifactOptions
    {
        public static Option Extract => new Option
        {
            OptionId = "Extract",
            Description = "Extract files",
            Value = "True"
        };

        public static Option Compress => new Option
        {
            OptionId = "Compress",
            Description = "Compress files",
            Value = "True"
        };
    }

    public static class StepArtifacts
    {
        public static StepArtifact Source => new StepArtifact
        {
            ArtifactId = "Source",
            Description = "Source Code",
            FileName = "Source.zip",
            Options = new List<Option>
            {
                StepArtifactOptions.Extract
            },
        };

        public static StepArtifact Binaries => new StepArtifact
        {
            ArtifactId = "Binaries",
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
            ArtifactId = "Results",
            Description = "Test Results",
            FileName = "Results.zip",
            OutputArtifact = true,
        };
    }

    public static class Steps
    {
        public static Step Build => new Step
        {
            StepId = "BuildStep",
            Description = "Build Step",
            Tags = new[] {"Build"},
            Requirements = new[] {"Docker", "Build"},
            InputArtifacts = new List<StepArtifact>
            {
                StepArtifacts.Source
            },
            OutputArtifacts = new List<StepArtifact>
            {
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
            StepId = "TestStep",
            Description = "Test Step",
            Dependencies = new[] {"BuildStep"},
            Tags = new[] {"TEST"},
            Requirements = new[] {"Windows","Database"},
            InputArtifacts = new List<StepArtifact>
            {
                StepArtifacts.Source
            },
            OutputArtifacts = new List<StepArtifact>
            {
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
}