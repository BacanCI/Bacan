using System;
using System.Collections.Generic;
using Bakana.Core.Entities;

namespace Bakana.IntegrationTests.TestData
{
    public static class Batches
    {
        public static Batch FullyPopulated => new Batch
        {
            BatchId = "Test",
            Description = "First",
            ExpiresOn = DateTime.Now,
            InputArtifacts = new List<BatchArtifact>
            {
                new BatchArtifact
                {
                    ArtifactId = "Package",
                    Description = "First artifact",
                    FileName = "package.zip",
                    Options = new List<BatchArtifactOption>
                    {
                        new BatchArtifactOption
                        {
                            OptionId = "OPT1",
                            Description = "First option",
                            Value = "OPT1VAL"
                        }
                    }
                }
            },
            Variables = new List<BatchVariable>
            {
                new BatchVariable
                {
                    VariableId = "VAR1",
                    Description = "First var",
                    Value = "VAR1VAL"
                },
                new BatchVariable
                {
                    VariableId = "VAR2",
                    Description = "Second var",
                    Value = "VAR2VAL",
                    Sensitive = true
                },
            },
            Options = new List<BatchOption>
            {
                new BatchOption
                {
                    OptionId = "BOPT",
                    Description = "Batch option 1",
                    Value = "BOPTVAL"
                }
            },
            Steps = new List<Step>
            {
                Steps.Build,
                Steps.Test
            }
        };
    }
}