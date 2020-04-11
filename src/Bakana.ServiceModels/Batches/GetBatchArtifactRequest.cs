using System.Collections.Generic;
using System.Net;
using Bakana.DomainModels;
using ServiceStack;

namespace Bakana.ServiceModels.Batches
{
    [Tag("Batch")]
    [Route("/batch/{BatchId}/artifact/{ArtifactName}", HttpMethods.Get, Summary = "Get Batch Artifact")]
    [ApiResponse(HttpStatusCode.NotFound, "The Batch or Batch Artifact was not found")]
    public class GetBatchArtifactRequest : IReturn<GetBatchArtifactResponse>
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
        public string ArtifactName { get; set; }
    }

    public class GetBatchArtifactResponse : IHasResponseStatus
    {
        [ApiMember(
            Description = "A user-generated identifier associated with the Artifact")]
        public string ArtifactName { get; set; }
        
        [ApiMember(
            Description = "A description of the Artifact")]
        public string Description { get; set; }

        [ApiMember(
            Description = "The Artifact's filename")]
        public string FileName { get; set; }

        [ApiMember(
            Description = "An array of Options associated with the Artifact")]
        public List<Option> Options { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
}