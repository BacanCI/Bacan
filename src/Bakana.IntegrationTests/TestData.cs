using System;
using System.Collections.Generic;
using Bakana.Core.Entities;

namespace Bakana.IntegrationTests
{
    public static class TestData
    {
        public static class Commands
        {
            public static Command DotNetRestore = new Command
            {
                Id = "CMD1",
                Description = "Command1",
                Item = "dot net restore",
                Variables = new List<CommandVariable>
                {
                    new CommandVariable
                    {
                        Description = "Command var 1",
                        Name = "C1",
                        Value = "C1V1"
                    }
                },
                Options = new List<CommandOption>
                {
                    new CommandOption
                    {
                        Description = "C1 Option",
                        Name = "C1OPT",
                        Value = "C1OPTVAL"
                    }
                }
            };

            public static Command DotNetBuild = new Command
            {
                Id = "CMD2",
                Description = "Command2",
                Item = "dot net build",
                Variables = new List<CommandVariable>
                {
                    new CommandVariable
                    {
                        Description = "Command var 2",
                        Name = "C2",
                        Value = "C2V1"
                    }
                },
                Options = new List<CommandOption>
                {
                    new CommandOption
                    {
                        Description = "C2 Option",
                        Name = "C2OPT",
                        Value = "C2OPTVAL"
                    }
                }
            };

        }
        
        public static class Batches
        {
            public static Batch FullyPopulated = new Batch
            {
                Description = "First",
                UserBatchId = "Test",
                ExpiresOn = DateTime.Now,
                InputArtifacts = new List<BatchArtifact>
                {
                    new BatchArtifact
                    {
                        Description = "First artifact",
                        FileName = "package.zip",
                        Options = new List<BatchArtifactOption>
                        {
                            new BatchArtifactOption
                            {
                                Description = "First option",
                                Name = "OPT1",
                                Value = "OPT1VAL"
                            }
                        }
                    }
                },
                Variables = new List<BatchVariable>
                {
                    new BatchVariable
                    {
                        Description = "First var",
                        Name = "VAR1",
                        Value = "VAR1VAL"
                    },
                    new BatchVariable
                    {
                        Description = "Second var",
                        Name = "VAR2",
                        Value = "VAR2VAL",
                        Sensitive = true
                    },
                },
                Options = new List<BatchOption>
                {
                    new BatchOption
                    {
                        Description = "Batch option 1",
                        Name = "BOPT",
                        Value = "BOPTVAL"
                    }
                },
                Steps = new List<Step>
                {
                    new Step
                    {
                        Id = "STEP1",
                        Description = "First step",
                        Dependencies = new[] {"0", "1"},
                        Tags = new[] {"Tag1", "Tag2"},
                        Requirements = new[] {"R1", "R2"},
                        InputArtifacts = new List<StepArtifact>
                        {
                            new StepArtifact
                            {
                                Description = "Package",
                                FileName = "StepPackage.zip",
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
                                Description = "Results",
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
                    }
                }
            };
        }
    }
}