using ServiceStack;
using System.Collections.Generic;

namespace Bakana.ServiceModels
{
    [Tag("Batch")]
    [Route("/batch", HttpMethods.Put, Summary = "Update batch")]
    public class UpdateBatchRequest : IReturn<CreateBatchResponse>
    {
        [ApiMember(Name = "Batch Id",
            Description = "A system generated value associated with the batch",
            ParameterType = "model",
            DataType = "string",
            IsRequired = false)]
        public string Id { get; set; }

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
    }

    public class UpdateBatchResponse : IHasResponseStatus
    {
        public ResponseStatus ResponseStatus { get; set; }
    }
}