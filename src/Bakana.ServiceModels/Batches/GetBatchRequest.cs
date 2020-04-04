using System.Collections.Generic;
using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Batches
{
    [Tag("Batch")]
    [Route("/batch/{BatchId}", HttpMethods.Get, Summary = "Get Batch")]
    [ApiResponse(HttpStatusCode.NotFound, "The Batch was not found")]
    public class GetBatchRequest : IReturn<GetBatchResponse>
    {
        [ApiMember(
            Description = "A system-generated identifier associated with the Batch",
            DataType = "string",
            ParameterType = "path",
            IsRequired = true)]
        public string BatchId { get; set; }
    }
    public class GetBatchResponse : IHasResponseStatus
    {
        [ApiMember(
            Description = "A system-generated identifier associated with the Batch")]
        public string BatchId { get; set; }

        [ApiMember(
            Description = "A description of the Batch")]
        public string Description { get; set; }

        [ApiMember(
            Description = "Options assigned to Batch")]
        public List<Option> Options { get; set; }

        [ApiMember(
            Description = "Array of variables to be used by all Steps in the Batch")]
        public List<Variable> Variables { get; set; }

        [ApiMember(
            Description = "An array of Artifacts associated with Batch")]
        public List<BatchArtifact> InputArtifacts { get; set; }

        [ApiMember(
            Description = "An array of Steps associated with Batch")]
        public List<Step> Steps { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
}