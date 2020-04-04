namespace Bakana.ServiceInterface
{
    public static class ErrMsg
    {
        public static string BatchNotFound(string batchId) => $"Batch {batchId} not found";

        public static string BatchVariableAlreadyExists(string variableId) => $"Batch Variable {variableId} already exists";
        public static string BatchVariableNotFound(string variableId) => $"Batch Variable {variableId} not found";

        public static string BatchOptionAlreadyExists(string optionId) => $"Batch Option {optionId} already exists";
        public static string BatchOptionNotFound(string optionId) => $"Batch Option {optionId} not found";

        public static string BatchArtifactAlreadyExists(string artifactId) => $"Batch Artifact {artifactId} already exists";
        public static string BatchArtifactNotFound(string artifactId) => $"Batch Artifact {artifactId} not found";
        
        public static string BatchArtifactOptionAlreadyExists(string optionId) => $"Batch Artifact Option {optionId} already exists";
        public static string BatchArtifactOptionNotFound(string optionId) => $"Batch Artifact Option {optionId} not found";
    }
}
