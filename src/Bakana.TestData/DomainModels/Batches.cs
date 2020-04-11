using System.Collections.Generic;
using Bakana.DomainModels;

namespace Bakana.TestData.DomainModels
{
    public static class BatchVariables
    {
        public static Variable Schedule => new Variable
        {
            Name = "Schedule",
            Description = "Schedule start",
            Value = "12:30:45"
        };
        
        public static Variable Environment => new Variable
        {
            Name = "Environment",
            Description = "Deployment Environment",
            Value = "Production"
        };
    }

    public static class BatchOptions
    {
        public static Option Debug => new Option
        {
            Name = "Debug",
            Description = "Debug Mode",
            Value = "True"
        };
        
        public static Option Log => new Option
        {
            Name = "Log",
            Description = "Verbose Logging",
            Value = "True"
        };
    }

    public static class BatchArtifactOptions
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

    public static class BatchArtifacts
    {
        public static BatchArtifact Package => new BatchArtifact
        {
            Name = "Package",
            Description = "First artifact",
            FileName = "package.zip",
            Options = new List<Option>
            {
                BatchArtifactOptions.Extract
            }
        };
        
        public static BatchArtifact DbBackup => new BatchArtifact
        {
            Name = "DbBackup",
            Description = "Database Backup",
            FileName = "db.zip",
            Options = new List<Option>
            {
                BatchArtifactOptions.Extract
            }
        };
    }

    public static class Batches
    {
        public static Batch FullyPopulated => new Batch
        {
            Description = "First",
            Artifacts = new List<BatchArtifact>
            {
                BatchArtifacts.Package
            },
            Variables = new List<Variable>
            {
                BatchVariables.Schedule
            },
            Options = new List<Option>
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
