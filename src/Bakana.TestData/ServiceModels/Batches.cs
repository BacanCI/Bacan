using Bakana.ServiceModels.Batches;
using Bakana.TestData.DomainModels;
using ServiceStack;

namespace Bakana.TestData.ServiceModels
{
    public static class CreateBatchVariables
    {
        public static CreateBatchVariableRequest Schedule =
            BatchVariables.Schedule.ConvertTo<CreateBatchVariableRequest>();
    
        public static CreateBatchVariableRequest Environment =
            BatchVariables.Environment.ConvertTo<CreateBatchVariableRequest>();
    }
    
    public static class UpdateBatchVariables
    {
        public static UpdateBatchVariableRequest Schedule =
            BatchVariables.Schedule.ConvertTo<UpdateBatchVariableRequest>();
    
        public static UpdateBatchVariableRequest Environment =
            BatchVariables.Environment.ConvertTo<UpdateBatchVariableRequest>();
    }

    public static class CreateBatchOptions
    {
        public static CreateBatchOptionRequest Debug =
            BatchOptions.Debug.ConvertTo<CreateBatchOptionRequest>();
    
        public static CreateBatchOptionRequest Log =
            BatchOptions.Log.ConvertTo<CreateBatchOptionRequest>();
    }
    
    public static class UpdateBatchOptions
    {
        public static UpdateBatchOptionRequest Debug =
            BatchOptions.Debug.ConvertTo<UpdateBatchOptionRequest>();
    
        public static UpdateBatchOptionRequest Log =
            BatchOptions.Log.ConvertTo<UpdateBatchOptionRequest>();
    }

    public static class GetBatchOptions
    {
        public static GetBatchOptionRequest Debug =
            BatchOptions.Debug.ConvertTo<GetBatchOptionRequest>();

        public static GetBatchOptionRequest Log =
            BatchOptions.Log.ConvertTo<GetBatchOptionRequest>();
    }

    public static class DeleteBatchOptions
    {
        public static DeleteBatchOptionRequest Debug =
            BatchOptions.Debug.ConvertTo<DeleteBatchOptionRequest>();

        public static DeleteBatchOptionRequest Log =
            BatchOptions.Log.ConvertTo<DeleteBatchOptionRequest>();
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

    public static class CreateBatches
    {
        public static CreateBatchRequest FullyPopulated => TestData.DomainModels.Batches.FullyPopulated.ConvertTo<CreateBatchRequest>();
    }

    public static class UpdateBatches
    {
        public static UpdateBatchRequest FullyPopulated => TestData.DomainModels.Batches.FullyPopulated.ConvertTo<UpdateBatchRequest>();
    }
}