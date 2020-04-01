using System;
using System.Collections.Generic;
using Bakana.Core.Entities;

namespace Bakana.IntegrationTests.TestData
{
    public static class Batches
    {
        public static class BatchVariables
        {
            public static BatchVariable Schedule => new BatchVariable
            {
                VariableId = "Schedule",
                Description = "Schedule start",
                Value = "12:30:45"
            };
        }
        
        public static class BatchOptions
        {
            public static BatchOption Debug => new BatchOption
            {
                OptionId = "Debug",
                Description = "Debug Mode",
                Value = "True"
            };
        }
        
        public static class BatchArtifactOptions
        {
            public static BatchArtifactOption Extract => new BatchArtifactOption
            {
                OptionId = "Extract",
                Description = "Extract files",
                Value = "True"
            };
        }
        
        public static class BatchArtifacts
        {
            public static BatchArtifact Package => new BatchArtifact
            {
                ArtifactId = "Package",
                Description = "First artifact",
                FileName = "package.zip",
                Options = new List<BatchArtifactOption>
                {
                    BatchArtifactOptions.Extract
                }
            };
        }
        
        public static Batch FullyPopulated => new Batch
        {
            BatchId = "Test",
            Description = "First",
            ExpiresOn = DateTime.Now,
            InputArtifacts = new List<BatchArtifact>
            {
                BatchArtifacts.Package
            },
            Variables = new List<BatchVariable>
            {
                BatchVariables.Schedule
            },
            Options = new List<BatchOption>
            {
                BatchOptions.Debug
            },
            Steps = new List<Step>
            {
                Steps.Build,
                Steps.Test
            }
        };
    }
}