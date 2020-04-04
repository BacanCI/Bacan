using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Batches
{
    [Tag("Batch")]
    [Route("/batch/{BatchId}/artifact/{ArtifactId}/option/{OptionId}", HttpMethods.Get, Summary = "Get Batch Artifact Option")]
    [ApiResponse(HttpStatusCode.NotFound, "The Batch or Batch Artifact or Batch Artifact Option was not found")]
    public class GetBatchArtifactOptionRequest : IReturn<GetBatchArtifactOptionResponse>
    {
        [ApiMember(
            Description = "A system-generated value associated with the Batch",
            DataType = "string",
            ParameterType = "path",
            IsRequired = true)]
        public string BatchId { get; set; }

        [ApiMember(
            Description = "A user-generated identifier associated with the Artifact",
            DataType = "string",
            ParameterType = "path",
            IsRequired = true)]
        public string ArtifactId { get; set; }
        
        [ApiMember(
            Description = "A user-generated identifier associated with the Artifact Option",
            DataType = "string",
            ParameterType = "path",
            IsRequired = true)]
        public string OptionId { get; set; }
    }

    public class GetBatchArtifactOptionResponse : IHasResponseStatus
    {
        [ApiMember(
            Description = "A user-generated identifier associated with the Artifact Option")]
        public string OptionId { get; set; }
        
        [ApiMember(
            Description = "A description of the Artifact Option")]
        public string Description { get; set; }

        [ApiMember(
            Description = "The value assigned to the Artifact Option")]
        public string Value { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
}