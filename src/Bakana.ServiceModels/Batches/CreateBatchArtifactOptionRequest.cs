using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Batches
{
    [Tag("Batch")]
    [Route("/batch/{BatchId}/artifact/{ArtifactId}/option", HttpMethods.Post, Summary = "Create new Batch Artifact Option")]
    [ApiResponse(HttpStatusCode.NotFound, "The Batch or Batch Artifact was not found")]
    [ApiResponse(HttpStatusCode.Conflict, "The Batch Artifact Option already exists")]
    public class CreateBatchArtifactOptionRequest : IReturn<CreateBatchArtifactOptionResponse>
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
            ParameterType = "model")]
        public string ArtifactId { get; set; }
        
        [ApiMember(
            Description = "A user-generated identifier associated with the Artifact Option",
            DataType = "string",
            ParameterType = "model")]
        public string OptionId { get; set; }

        [ApiMember(
            Description = "A description of the Artifact Option",
            DataType = "string",
            ParameterType = "model")]
        public string Description { get; set; }

        [ApiMember(
            Description = "The value assigned to the Artifact Option",
            DataType = "string",
            ParameterType = "model",
            IsRequired = true)]
        public string Value { get; set; }
    }

    public class CreateBatchArtifactOptionResponse : IHasResponseStatus
    {
        public ResponseStatus ResponseStatus { get; set; }
    }
}