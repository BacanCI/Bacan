using System.Collections.Generic;
using Bakana.DomainModels;
using ServiceStack;

namespace Bakana.ServiceModels.Batches
{
    [Tag("Batch")]
    [Route("/batch", HttpMethods.Post, Summary = "Create new Batch")]
    public class CreateBatchRequest : IReturn<CreateBatchResponse>
    {
        [ApiMember(
            Description = "A description of the Batch",
            ParameterType = "model",
            DataType = "string",
            IsRequired = false)]
        public string Description { get; set; }

        [ApiMember(
            Description = "Options to assign to Batch",
            ParameterType = "model",
            IsRequired = false)]
        public List<Option> Options { get; set; }

        [ApiMember(
            Description = "Array of variables to be used by all steps in the Batch",
            ParameterType = "model",
            IsRequired = false)]
        public List<Variable> Variables { get; set; }

        [ApiMember(
            Description = "An array of artifacts associated with Batch",
            ParameterType = "model",
            IsRequired = false)]
        public List<BatchArtifact> Artifacts { get; set; }
        
        [ApiMember(
            Description = "An array of steps associated with Batch",
            ParameterType = "model",
            IsRequired = false)]
        public List<Step> Steps { get; set; }
    }

    public class CreateBatchResponse : IHasResponseStatus
    {
        [ApiMember(
            Description = "A system-generated identifier associated with the Batch")]
        public string BatchId { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
}