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
    }
}