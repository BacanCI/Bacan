using System;
using ServiceStack;

namespace Bakana.ServiceInterface
{
    public abstract class BakanaService : Service
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
}