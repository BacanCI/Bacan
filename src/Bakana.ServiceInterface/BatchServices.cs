using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bakana.Core;
using Bakana.Core.Entities;
using Bakana.Core.Repositories;
using Bakana.ServiceModels;
using ServiceStack;
using ServiceStack.Text;
using Command = Bakana.Core.Entities.Command;

namespace Bakana.ServiceInterface
{
    public class BatchServices : Service
    {
        private readonly IShortIdGenerator idGenerator;
        private readonly IBatchRepository batchRepository;

        public BatchServices(
            IShortIdGenerator idGenerator, 
            IBatchRepository batchRepository)
        {
            this.idGenerator = idGenerator;
            this.batchRepository = batchRepository;
        }
        
        public async Task<CreateBatchResponse> Post(CreateBatchRequest request)
        {
            //var batch = request.ConvertTo<Batch>();
            var batch = new Batch
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
                            new Command
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
                            },
                            new Command
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
                            }
                        },
                    }
                }
            };

            batch.Id = idGenerator.Generate();

            await batchRepository.CreateOrUpdate(batch);

            var result = await batchRepository.Get(batch.Id);

            var json = result.ToJson();
            
            return new CreateBatchResponse
            {
                Id = batch.Id
            };
        }
    }
}
