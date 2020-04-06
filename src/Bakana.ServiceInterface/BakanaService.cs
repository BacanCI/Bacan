using System.Net;
using ServiceStack;

namespace Bakana.ServiceInterface
{
    public abstract class BakanaService : Service
    {
        public static HttpError BatchNotFound(string batchId) =>
            new HttpError(HttpStatusCode.NotFound, ErrMsg.BatchNotFound(batchId));
        public static HttpError BatchVariableAlreadyExists(string variableId) =>
            new HttpError(HttpStatusCode.Conflict, ErrMsg.BatchVariableAlreadyExists(variableId));
        public static HttpError BatchVariableNotFound(string variableId) =>
            new HttpError(HttpStatusCode.NotFound, ErrMsg.BatchVariableNotFound(variableId));
        public static HttpError BatchOptionAlreadyExists(string optionId) =>
            new HttpError(HttpStatusCode.Conflict, ErrMsg.BatchOptionAlreadyExists(optionId));
        public static HttpError BatchOptionNotFound(string optionId) =>
            new HttpError(HttpStatusCode.NotFound, ErrMsg.BatchOptionNotFound(optionId));
        public static HttpError BatchArtifactAlreadyExists(string artifactId) =>
            new HttpError(HttpStatusCode.Conflict, ErrMsg.BatchArtifactAlreadyExists(artifactId));
        public static HttpError BatchArtifactNotFound(string artifactId) =>
            new HttpError(HttpStatusCode.NotFound, ErrMsg.BatchArtifactNotFound(artifactId));
        public static HttpError BatchArtifactOptionAlreadyExists(string optionId) =>
            new HttpError(HttpStatusCode.Conflict, ErrMsg.BatchArtifactOptionAlreadyExists(optionId));
        public static HttpError BatchArtifactOptionNotFound(string optionId) =>
            new HttpError(HttpStatusCode.NotFound, ErrMsg.BatchArtifactOptionNotFound(optionId));
        
        public static HttpError StepAlreadyExists(string stepId) =>
            new HttpError(HttpStatusCode.Conflict, ErrMsg.StepAlreadyExists(stepId));
        public static HttpError StepNotFound(string stepId) =>
            new HttpError(HttpStatusCode.NotFound, ErrMsg.StepNotFound(stepId));
        public static HttpError StepVariableAlreadyExists(string variableId) =>
            new HttpError(HttpStatusCode.Conflict, ErrMsg.StepVariableAlreadyExists(variableId));
        public static HttpError StepVariableNotFound(string variableId) =>
            new HttpError(HttpStatusCode.NotFound, ErrMsg.StepVariableNotFound(variableId));
        public static HttpError StepOptionAlreadyExists(string optionId) =>
            new HttpError(HttpStatusCode.Conflict, ErrMsg.StepOptionAlreadyExists(optionId));
        public static HttpError StepOptionNotFound(string optionId) =>
            new HttpError(HttpStatusCode.NotFound, ErrMsg.StepOptionNotFound(optionId));
        public static HttpError StepArtifactAlreadyExists(string artifactId) =>
            new HttpError(HttpStatusCode.Conflict, ErrMsg.StepArtifactAlreadyExists(artifactId));
        public static HttpError StepArtifactNotFound(string artifactId) =>
            new HttpError(HttpStatusCode.NotFound, ErrMsg.StepArtifactNotFound(artifactId));
        public static HttpError StepArtifactOptionAlreadyExists(string optionId) =>
            new HttpError(HttpStatusCode.Conflict, ErrMsg.StepArtifactOptionAlreadyExists(optionId));
        public static HttpError StepArtifactOptionNotFound(string optionId) =>
            new HttpError(HttpStatusCode.NotFound, ErrMsg.StepArtifactOptionNotFound(optionId));
        
        public static HttpError CommandAlreadyExists(string commandId) =>
            new HttpError(HttpStatusCode.Conflict, ErrMsg.CommandAlreadyExists(commandId));
        public static HttpError CommandNotFound(string commandId) =>
            new HttpError(HttpStatusCode.NotFound, ErrMsg.CommandNotFound(commandId));
        public static HttpError CommandVariableAlreadyExists(string variableId) =>
            new HttpError(HttpStatusCode.Conflict, ErrMsg.CommandVariableAlreadyExists(variableId));
        public static HttpError CommandVariableNotFound(string variableId) =>
            new HttpError(HttpStatusCode.NotFound, ErrMsg.CommandVariableNotFound(variableId));
        public static HttpError CommandOptionAlreadyExists(string optionId) =>
            new HttpError(HttpStatusCode.Conflict, ErrMsg.CommandOptionAlreadyExists(optionId));
        public static HttpError CommandOptionNotFound(string optionId) =>
            new HttpError(HttpStatusCode.NotFound, ErrMsg.CommandOptionNotFound(optionId));
    }
}