using System.Collections.Generic;
using Bakana.Core.Entities;

namespace Bakana.IntegrationTests.TestData
{
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
                new StepArtifact
                {
                    ArtifactId = "Source",
                    Description = "Source Code",
                    FileName = "Source.zip",
                    Options = new List<StepArtifactOption>
                    {
                        new StepArtifactOption
                        {
                            OptionId = "Art1",
                            Description = "First artifact option",
                            Value = "Art1Val"
                        }
                    },
                }
            },
            OutputArtifacts = new List<StepArtifact>
            {
                new StepArtifact
                {
                    ArtifactId = "Binaries",
                    Description = "Binaries",
                    FileName = "Build.zip",
                    OutputArtifact = true,
                    Options = new List<StepArtifactOption>
                    {
                        new StepArtifactOption
                        {
                            OptionId = "Res1",
                            Description = "First result option",
                            Value = "Res1Val"
                        }
                    },
                }
            },
            Options = new List<StepOption>
            {
                new StepOption
                {
                    OptionId = "S1OPT",
                    Description = "Step1 option",
                    Value = "S1OPTVAL"
                }
            },
            Variables = new List<StepVariable>
            {
                new StepVariable
                {
                    VariableId = "S1V1",
                    Description = "Step1 var1",
                    Value = "S1V1VAL"
                }
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
                new StepArtifact
                {
                    ArtifactId = "Binaries",
                    Description = "Binaries",
                    FileName = "Build.zip",
                    Options = new List<StepArtifactOption>
                    {
                        new StepArtifactOption
                        {
                            OptionId = "Extract",
                            Description = "Extract files",
                            Value = "True"
                        }
                    },
                }
            },
            OutputArtifacts = new List<StepArtifact>
            {
                new StepArtifact
                {
                    ArtifactId = "Results",
                    Description = "Test Results",
                    FileName = "Results.zip",
                    OutputArtifact = true,
                    Options = new List<StepArtifactOption>
                    {
                        new StepArtifactOption
                        {
                            OptionId = "Res1",
                            Description = "First result option",
                            Value = "Res1Val"
                        }
                    },
                }
            },
            Options = new List<StepOption>
            {
                new StepOption
                {
                    OptionId = "S2OPT",
                    Description = "Step2 option",
                    Value = "S2OPTVAL"
                }
            },
            Variables = new List<StepVariable>
            {
                new StepVariable
                {
                    VariableId = "S2V1",
                    Description = "Step2 var1",
                    Value = "S2V1VAL"
                }
            },
            Commands = new List<Command>
            {
                Commands.Deploy,
                Commands.Test,
            },
        };

    }
}