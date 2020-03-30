using System.Collections.Generic;
using ServiceStack;

namespace Bakana.ServiceModels
{
    [Tag("Batch")]
    [Route("/batch", HttpMethods.Post, Summary = "Create new batch")]
    public class CreateBatchRequest : IReturn<CreateBatchResponse>
    {
        [ApiMember(Name="User Batch Id", Description = "A user-defined value associated with the batch",
            ParameterType = "model", DataType = "string", IsRequired = false)]
        public string UserBatchId { get; set; }
        
        [ApiMember(Name="Description", Description = "A description of the batch",
            ParameterType = "model", DataType = "string", IsRequired = false)]
        public string Description { get; set; }
        
        [ApiMember(Name="Options", Description = "Key/value pair settings applied to the batch", 
            ParameterType = "model", IsRequired = false)]
        public Dictionary<string, string> Options { get; set; }
        
        [ApiMember(Name="Variables", Description = "Key/value pairs variables applied to the batch", 
            ParameterType = "model", IsRequired = false)]
        public Dictionary<string, string> Variables { get; set; }
    }

    public class CreateBatchResponse : IHasResponseStatus
    {
        [ApiMember(Name="Id", Description = "Auto-generated batch id")]
        public string Id { get; set; }
        
        public ResponseStatus ResponseStatus { get; set; }
    }
}