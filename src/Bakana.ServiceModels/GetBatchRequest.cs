using ServiceStack;
using System.Collections.Generic;

namespace Bakana.ServiceModels
{
    [Tag("Batch")]
    [Route("/batch", HttpMethods.Get, Summary = "Get batch")]
    public class GetBatchRequest : IReturn<GetBatchResponse>
    {
        [ApiMember(Name = "Batch Id",
            Description = "A user-defined value associated with the batch",
            ParameterType = "model",
            DataType = "string",
            IsRequired = false)]
        public string BatchId { get; set; }
    }
    public class GetBatchResponse : IHasResponseStatus
    {
        [ApiMember(Name = "Batch Id",
            Description = "A user-defined value associated with the batch",
            ParameterType = "model",
            DataType = "string",
            IsRequired = false)]
        public string BatchId { get; set; }

        [ApiMember(Name = "Description",
            Description = "A description of the batch",
            ParameterType = "model",
            DataType = "string",
            IsRequired = false)]
        public string Description { get; set; }

        [ApiMember(Name = "Options",
            Description = "Options to assign to batch",
            ParameterType = "model",
            IsRequired = false)]
        public List<Option> Options { get; set; }

        [ApiMember(Name = "Variables",
            Description = "Array of variables to be used by all steps in the batch",
            ParameterType = "model",
            IsRequired = false)]
        public List<Variable> Variables { get; set; }

        [ApiMember(Name = "Input Artifacts",
            Description = "An array of artifacts associated with batch",
            ParameterType = "model",
            IsRequired = false)]
        public List<BatchArtifact> InputArtifacts { get; set; }

        [ApiMember(Name = "Steps",
            Description = "An array of steps to append to batch",
            ParameterType = "model",
            IsRequired = false)]
        public List<Step> Steps { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
}