using System.Collections.Generic;
using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Batches
{
    [Tag("Batch")]
    [Route("/batch/{BatchId}", HttpMethods.Get, Summary = "Get batch")]
    [ApiResponse(HttpStatusCode.NotFound, "The batch was not found")]
    public class GetBatchRequest : IReturn<GetBatchResponse>
    {
        [ApiMember(
            Description = "A system-generated value associated with the batch",
            DataType = "string",
            ParameterType = "path",
            IsRequired = true)]
        public string BatchId { get; set; }
    }
    public class GetBatchResponse : IHasResponseStatus
    {
        [ApiMember(
            Description = "A system-generated value associated with the batch")]
        public string BatchId { get; set; }

        [ApiMember(
            Description = "A description of the batch")]
        public string Description { get; set; }

        [ApiMember(
            Description = "Options assigned to batch")]
        public List<Option> Options { get; set; }

        [ApiMember(
            Description = "Array of variables to be used by all steps in the batch")]
        public List<Variable> Variables { get; set; }

        [ApiMember(
            Description = "An array of artifacts associated with batch")]
        public List<BatchArtifact> InputArtifacts { get; set; }

        [ApiMember(
            Description = "An array of steps associated with batch")]
        public List<Step> Steps { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
}