using ServiceStack;
using System.Collections.Generic;

namespace Bakana.ServiceModels
{
    [Tag("Batch")]
    [Route("/batch/{BatchId}", HttpMethods.Get, Summary = "Get batch")]
    public class GetBatchRequest : IReturn<GetBatchResponse>
    {
        [ApiMember(Name = "BatchId",
            Description = "A system-generated value associated with the batch",
            DataType = "string",
            ParameterType = "path",
            IsRequired = true)]
        public string BatchId { get; set; }
    }
    public class GetBatchResponse : IHasResponseStatus
    {
        [ApiMember(Name = "Batch Id",
            Description = "A system-generated value associated with the batch",
            DataType = "string")]
        public string BatchId { get; set; }

        [ApiMember(Name = "Description",
            Description = "A description of the batch",
            DataType = "string")]
        public string Description { get; set; }

        [ApiMember(Name = "Options",
            Description = "Options assigned to batch")]
        public List<Option> Options { get; set; }

        [ApiMember(Name = "Variables",
            Description = "Array of variables to be used by all steps in the batch")]
        public List<Variable> Variables { get; set; }

        [ApiMember(Name = "Input Artifacts",
            Description = "An array of artifacts associated with batch")]
        public List<BatchArtifact> InputArtifacts { get; set; }

        [ApiMember(Name = "Steps",
            Description = "An array of steps associated with batch")]
        public List<Step> Steps { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
}