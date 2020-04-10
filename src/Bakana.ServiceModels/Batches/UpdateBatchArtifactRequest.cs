using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Batches
{
    [Tag("Batch")]
    [Route("/batch/{BatchId}/artifact/{ArtifactName}", HttpMethods.Put, Summary = "Update Batch Artifact")]
    [ApiResponse(HttpStatusCode.NotFound, "The Batch or Batch Artifact was not found")]
    public class UpdateBatchArtifactRequest : IReturn<UpdateBatchArtifactResponse>
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
        
        [ApiMember(
            Description = "A description of the Artifact",
            DataType = "string",
            ParameterType = "model")]
        public string Description { get; set; }

        [ApiMember(
            Description = "The Artifact's filename",
            DataType = "string",
            ParameterType = "model",
            IsRequired = true)]
        public string FileName { get; set; }
    }

    public class UpdateBatchArtifactResponse : IHasResponseStatus
    {
        public ResponseStatus ResponseStatus { get; set; }
    }
}