using System.Collections.Generic;
using Bakana.DomainModels;
using Command = Bakana.DomainModels.Command;

namespace Bakana.TestData.DomainModels
{
    public static class StepVariables
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
}