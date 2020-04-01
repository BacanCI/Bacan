using System;
using System.Collections.Generic;
using Bakana.Core.Entities;

namespace Bakana.IntegrationTests.TestData
{
    public static class Batches
    {
        public static Batch FullyPopulated => new Batch
        {
            Description = "First",
            BatchId = "Test",
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
                Steps.Build,
                Steps.Test
            }
        };
    }
}