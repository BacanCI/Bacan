using System.Collections.Generic;
using Bakana.ServiceModels;
using Bakana.ServiceModels.Batches;
using ServiceStack;

namespace Bakana.TestData.ServiceModels
{
    public static class BatchVariables
    {
        public static Variable Schedule => new Variable
        {
            VariableId = "Schedule",
            Description = "Schedule start",
            Value = "12:30:45"
        };
        
        public static Variable Environment => new Variable
        {
            VariableId = "Environment",
            Description = "Deployment Environment",
            Value = "Production"
        };
    }
        
    public static class BatchOptions
    {
        public static Option Debug => new Option
        {
            OptionId = "Debug",
            Description = "Debug Mode",
            Value = "True"
        };
        
        public static Option Log => new Option
        {
            OptionId = "Log",
            Description = "Verbose Logging",
            Value = "True"
        };
    }
        
    public static class BatchArtifactOptions
    {
        public static Option Extract => new Option
        {
            OptionId = "Extract",
            Description = "Extract files",
            Value = "True"
        };
        
        public static Option Compress => new Option
        {
            OptionId = "Compress",
            Description = "Compress files",
            Value = "True"
        };
    }

    public static class CreateBatchArtifactOptions
    {
        public static CreateBatchArtifactOptionRequest Extract =
            BatchArtifactOptions.Extract.ConvertTo<CreateBatchArtifactOptionRequest>();
        
        public static CreateBatchArtifactOptionRequest Compress =
            BatchArtifactOptions.Compress.ConvertTo<CreateBatchArtifactOptionRequest>();
    }
        
    public static class UpdateBatchArtifactOptions
    {
        public static UpdateBatchArtifactOptionRequest Extract =
            BatchArtifactOptions.Extract.ConvertTo<UpdateBatchArtifactOptionRequest>();
        
        public static UpdateBatchArtifactOptionRequest Compress =
            BatchArtifactOptions.Compress.ConvertTo<UpdateBatchArtifactOptionRequest>();
    }

    public static class BatchArtifacts
    {
        public static BatchArtifact Package => new BatchArtifact
        {
            ArtifactId = "Package",
            Description = "First artifact",
            FileName = "package.zip",
            Options = new List<Option>
            {
                BatchArtifactOptions.Extract
            }
        };
        
        public static BatchArtifact DbBackup => new BatchArtifact
        {
            ArtifactId = "DbBackup",
            Description = "Database Backup",
            FileName = "db.zip",
            Options = new List<Option>
            {
                BatchArtifactOptions.Extract
            }
        };
    }

    public static class CreateBatchArtifacts
    {
        public static CreateBatchArtifactRequest Package => BatchArtifacts.Package.ConvertTo<CreateBatchArtifactRequest>();
        
        public static CreateBatchArtifactRequest DbBackup => BatchArtifacts.DbBackup.ConvertTo<CreateBatchArtifactRequest>();
    }
    
    public static class UpdateBatchArtifacts
    {
        public static UpdateBatchArtifactRequest Package => BatchArtifacts.Package.ConvertTo<UpdateBatchArtifactRequest>();
        
        public static UpdateBatchArtifactRequest DbBackup => BatchArtifacts.DbBackup.ConvertTo<UpdateBatchArtifactRequest>();
    }

    public static class Batches
    {
        public static CreateBatchRequest FullyPopulated => new CreateBatchRequest
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

    public static class UpdateBatches
    {
        public static UpdateBatchRequest FullyPopulated => Batches.FullyPopulated.ConvertTo<UpdateBatchRequest>();
    }
}
