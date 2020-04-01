using ServiceStack;

namespace Bakana.ServiceModels
{
    [Tag("Batch")]
    [Route("/batch", HttpMethods.Put, Summary = "Update batch")]
    public class UpdateBatchRequest : IReturn<CreateBatchResponse>
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
    }

    public class UpdateBatchResponse : IHasResponseStatus
    {
        public ResponseStatus ResponseStatus { get; set; }
    }
}