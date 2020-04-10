using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Batches
{
    [Tag("Batch")]
    [Route("/batch/{BatchId}/artifact/{ArtifactName}/option/{OptionName}", HttpMethods.Delete, Summary = "Delete Batch Artifact Option")]
    [ApiResponse(HttpStatusCode.NotFound, "The Batch or Batch Artifact or Batch Artifact Option was not found")]
    public class DeleteBatchArtifactOptionRequest : IReturn<DeleteBatchArtifactOptionResponse>
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
            Description = "A user-generated identifier associated with the Artifact Option",
            DataType = "string",
            ParameterType = "path",
            IsRequired = true)]
        public string OptionName { get; set; }
    }

    public class DeleteBatchArtifactOptionResponse : IHasResponseStatus
    {
        public ResponseStatus ResponseStatus { get; set; }
    }
}