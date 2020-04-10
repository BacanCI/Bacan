using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Batches
{
    [Tag("Batch")]
    [Route("/batch/{BatchId}/artifact/{ArtifactName}", HttpMethods.Delete, Summary = "Delete Batch Artifact")]
    [ApiResponse(HttpStatusCode.NotFound, "The Batch or Batch Artifact was not found")]
    public class DeleteBatchArtifactRequest : IReturn<DeleteBatchArtifactResponse>
    {
        [ApiMember(
            Description = "A system-generated identifier associated with the Batch",
            DataType = "string",
            ParameterType = "path",
            IsRequired = true)]
        public string BatchId { get; set; }

        [ApiMember(
            Description = "A user-generated identifier associated with the Artifact",
            DataType = "string",
            ParameterType = "path",
            IsRequired = true)]
        public string ArtifactName { get; set; }
    }

    public class DeleteBatchArtifactResponse : IHasResponseStatus
    {
        public ResponseStatus ResponseStatus { get; set; }
    }
}