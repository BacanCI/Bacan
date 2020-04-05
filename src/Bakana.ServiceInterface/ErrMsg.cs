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

        public static string StepNotFound(string stepId) => $"Step {stepId} not found";
        public static string StepVariableAlreadyExists(string variableId) => $"Step Variable {variableId} already exists";
        public static string StepVariableNotFound(string variableId) => $"Step Variable {variableId} not found";
        public static string StepOptionAlreadyExists(string optionId) => $"Step Option {optionId} already exists";
        public static string StepOptionNotFound(string optionId) => $"Step Option {optionId} not found";
        public static string StepArtifactAlreadyExists(string artifactId) => $"Step Artifact {artifactId} already exists";
        public static string StepArtifactNotFound(string artifactId) => $"Step Artifact {artifactId} not found";
        public static string StepArtifactOptionAlreadyExists(string optionId) => $"Step Artifact Option {optionId} already exists";
        public static string StepArtifactOptionNotFound(string optionId) => $"Step Artifact Option {optionId} not found";

        public static string CommandNotFound(string commandId) => $"Command {commandId} not found";
        public static string CommandVariableAlreadyExists(string variableId) => $"Command Variable {variableId} already exists";
        public static string CommandVariableNotFound(string variableId) => $"Command Variable {variableId} not found";
        public static string CommandOptionAlreadyExists(string optionId) => $"Command Option {optionId} already exists";
        public static string CommandOptionNotFound(string optionId) => $"Command Option {optionId} not found";
    }
}
