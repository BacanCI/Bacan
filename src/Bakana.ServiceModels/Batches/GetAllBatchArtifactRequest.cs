using System.Collections.Generic;
using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Batches
{
    [Tag("Batch")]
    [Route("/batch/{BatchId}/artifacts", HttpMethods.Get, Summary = "Get all Batch Artifacts")]
    [ApiResponse(HttpStatusCode.NotFound, "The Batch was not found")]
    public class GetAllBatchArtifactRequest : IReturn<GetAllBatchArtifactResponse>
    {
        [ApiMember(
            Description = "A system-generated identifier associated with the Batch",
            DataType = "string",
            ParameterType = "path",
            IsRequired = true)]
        public string BatchId { get; set; }
    }

    public class GetAllBatchArtifactResponse : IHasResponseStatus
    {
        public List<BatchArtifact> Artifacts { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
}