using System.Collections.Generic;
using Bakana.Core.Entities;

namespace Bakana.TestData.Entities
{
    public class StepVariables
    {
        public static StepVariable SourcePath => new StepVariable
        {
            Name = "SourcePath",
            Description = "Path to Source code",
            Value = "./src"
        };

        public static StepVariable Profile => new StepVariable
        {
            Name = "Profile",
            Description = "Build Profile ",
            Value = "PRODUCTION"
        };
        
        public static StepVariable TestPath => new StepVariable
        {
            Name = "TestPath",
            Description = "Path to test project",
            Value = "./tests"
        };
        
        public static StepVariable TestFilter => new StepVariable
        {
            Name = "TestFilter",
            Description = "NUnit Test Filter",
            Value = "--category = 'agency'"
        };
    }
    
    public class StepOptions
    {
        public static StepOption BuildAlways => new StepOption
        {
            Name = "BuildAlways",
            Description = "Build Always",
            Value = "True"
        };

        public static StepOption BuildWhenNoErrors => new StepOption
        {
            Name = "BuildWhenNoErrors",
            Description = "Build only when no previous step has errored",
            Value = "True"
        };
    }
    
    public static class StepArtifactOptions
    {
        public static StepArtifactOption Extract => new StepArtifactOption
        {
            Name = "Extract",
            Description = "Extract files",
            Value = "True"
        };

        public static StepArtifactOption Compress => new StepArtifactOption
        {
            Name = "Compress",
            Description = "Compress files",
            Value = "True"
        };
    }

    public static class StepArtifacts
    {
        public static StepArtifact Source => new StepArtifact
        {
            Name = "Source",
            Description = "Source Code",
            FileName = "Source.zip",
            Options = new List<StepArtifactOption>
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
            Options = new List<StepArtifactOption>
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
            Options = new List<StepOption>
            {
                StepOptions.BuildAlways,
            },
            Variables = new List<StepVariable>
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
            Options = new List<StepOption>
            {
                StepOptions.BuildAlways
            },
            Variables = new List<StepVariable>
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