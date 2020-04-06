using System;
using ServiceStack;

namespace Bakana.ServiceInterface
{
    public class Err
    {
        public static Exception BatchNotFound(string batchId) =>
            HttpError.NotFound(ErrMsg.BatchNotFound(batchId));
        public static Exception BatchVariableAlreadyExists(string variableId) =>
            HttpError.Conflict(ErrMsg.BatchVariableAlreadyExists(variableId));
        public static Exception BatchVariableNotFound(string variableId) =>
            HttpError.NotFound(ErrMsg.BatchVariableNotFound(variableId));
        public static Exception BatchOptionAlreadyExists(string optionId) =>
            HttpError.Conflict(ErrMsg.BatchOptionAlreadyExists(optionId));
        public static Exception BatchOptionNotFound(string optionId) =>
            HttpError.NotFound(ErrMsg.BatchOptionNotFound(optionId));
        public static Exception BatchArtifactAlreadyExists(string artifactId) =>
            HttpError.Conflict(ErrMsg.BatchArtifactAlreadyExists(artifactId));
        public static Exception BatchArtifactNotFound(string artifactId) =>
            HttpError.NotFound(ErrMsg.BatchArtifactNotFound(artifactId));
        public static Exception BatchArtifactOptionAlreadyExists(string optionId) =>
            HttpError.Conflict(ErrMsg.BatchArtifactOptionAlreadyExists(optionId));
        public static Exception BatchArtifactOptionNotFound(string optionId) =>
            HttpError.NotFound(ErrMsg.BatchArtifactOptionNotFound(optionId));
        
        public static Exception StepAlreadyExists(string stepId) =>
            HttpError.Conflict(ErrMsg.StepAlreadyExists(stepId));
        public static Exception StepNotFound(string stepId) =>
            HttpError.NotFound(ErrMsg.StepNotFound(stepId));
        public static Exception StepVariableAlreadyExists(string variableId) =>
            HttpError.Conflict(ErrMsg.StepVariableAlreadyExists(variableId));
        public static Exception StepVariableNotFound(string variableId) =>
            HttpError.NotFound(ErrMsg.StepVariableNotFound(variableId));
        public static Exception StepOptionAlreadyExists(string optionId) =>
            HttpError.Conflict(ErrMsg.StepOptionAlreadyExists(optionId));
        public static Exception StepOptionNotFound(string optionId) =>
            HttpError.NotFound(ErrMsg.StepOptionNotFound(optionId));
        public static Exception StepArtifactAlreadyExists(string artifactId) =>
            HttpError.Conflict(ErrMsg.StepArtifactAlreadyExists(artifactId));
        public static Exception StepArtifactNotFound(string artifactId) =>
            HttpError.NotFound(ErrMsg.StepArtifactNotFound(artifactId));
        public static Exception StepArtifactOptionAlreadyExists(string optionId) =>
            HttpError.Conflict(ErrMsg.StepArtifactOptionAlreadyExists(optionId));
        public static Exception StepArtifactOptionNotFound(string optionId) =>
            HttpError.NotFound(ErrMsg.StepArtifactOptionNotFound(optionId));
        
        public static Exception CommandAlreadyExists(string commandId) =>
            HttpError.Conflict(ErrMsg.CommandAlreadyExists(commandId));
        public static Exception CommandNotFound(string commandId) =>
            HttpError.NotFound(ErrMsg.CommandNotFound(commandId));
        public static Exception CommandVariableAlreadyExists(string variableId) =>
            HttpError.Conflict(ErrMsg.CommandVariableAlreadyExists(variableId));
        public static Exception CommandVariableNotFound(string variableId) =>
            HttpError.NotFound(ErrMsg.CommandVariableNotFound(variableId));
        public static Exception CommandOptionAlreadyExists(string optionId) =>
            HttpError.Conflict(ErrMsg.CommandOptionAlreadyExists(optionId));
        public static Exception CommandOptionNotFound(string optionId) =>
            HttpError.NotFound(ErrMsg.CommandOptionNotFound(optionId));
    }
    
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

        public static string StepAlreadyExists(string stepId) => $"Step {stepId} already exists";
        public static string StepNotFound(string stepId) => $"Step {stepId} not found";
        public static string StepVariableAlreadyExists(string variableId) => $"Step Variable {variableId} already exists";
        public static string StepVariableNotFound(string variableId) => $"Step Variable {variableId} not found";
        public static string StepOptionAlreadyExists(string optionId) => $"Step Option {optionId} already exists";
        public static string StepOptionNotFound(string optionId) => $"Step Option {optionId} not found";
        public static string StepArtifactAlreadyExists(string artifactId) => $"Step Artifact {artifactId} already exists";
        public static string StepArtifactNotFound(string artifactId) => $"Step Artifact {artifactId} not found";
        public static string StepArtifactOptionAlreadyExists(string optionId) => $"Step Artifact Option {optionId} already exists";
        public static string StepArtifactOptionNotFound(string optionId) => $"Step Artifact Option {optionId} not found";

        public static string CommandAlreadyExists(string commandId) => $"Command {commandId} already exists";
        public static string CommandNotFound(string commandId) => $"Command {commandId} not found";
        public static string CommandVariableAlreadyExists(string variableId) => $"Command Variable {variableId} already exists";
        public static string CommandVariableNotFound(string variableId) => $"Command Variable {variableId} not found";
        public static string CommandOptionAlreadyExists(string optionId) => $"Command Option {optionId} already exists";
        public static string CommandOptionNotFound(string optionId) => $"Command Option {optionId} not found";
    }
}