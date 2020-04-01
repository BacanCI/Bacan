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
                    Description = "Source Code",
                    FileName = "Source.zip",
                    Options = new List<StepArtifactOption>
                    {
                        new StepArtifactOption
                        {
                            Description = "First artifact option",
                            Name = "Art1",
                            Value = "Art1Val"
                        }
                    },
                }
            },
            OutputArtifacts = new List<StepArtifact>
            {
                new StepArtifact
                {
                    Description = "Binaries",
                    FileName = "Build.zip",
                    OutputArtifact = true,
                    Options = new List<StepArtifactOption>
                    {
                        new StepArtifactOption
                        {
                            Description = "First result option",
                            Name = "Res1",
                            Value = "Res1Val"
                        }
                    },
                }
            },
            Options = new List<StepOption>
            {
                new StepOption
                {
                    Description = "Step1 option",
                    Name = "S1OPT",
                    Value = "S1OPTVAL"
                }
            },
            Variables = new List<StepVariable>
            {
                new StepVariable
                {
                    Description = "Step1 var1",
                    Name = "S1V1",
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
                    Description = "Binaries",
                    FileName = "Build.zip",
                    Options = new List<StepArtifactOption>
                    {
                        new StepArtifactOption
                        {
                            Description = "Extract files",
                            Name = "Extract",
                            Value = "True"
                        }
                    },
                }
            },
            OutputArtifacts = new List<StepArtifact>
            {
                new StepArtifact
                {
                    Description = "Test Results",
                    FileName = "Results.zip",
                    OutputArtifact = true,
                    Options = new List<StepArtifactOption>
                    {
                        new StepArtifactOption
                        {
                            Description = "First result option",
                            Name = "Res1",
                            Value = "Res1Val"
                        }
                    },
                }
            },
            Options = new List<StepOption>
            {
                new StepOption
                {
                    Description = "Step2 option",
                    Name = "S2OPT",
                    Value = "S2OPTVAL"
                }
            },
            Variables = new List<StepVariable>
            {
                new StepVariable
                {
                    Description = "Step2 var1",
                    Name = "S2V1",
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