using ServiceStack;

namespace Bakana.ServiceModels
{
    [Tag("Batch")]
    [Route("/batch", HttpMethods.Delete, Summary = "Delete batch")]
    public class DeleteBatchRequest : IReturn<GetBatchResponse>
    {
        [ApiMember(Name = "Batch Id",
            Description = "A user-defined value associated with the batch",
            ParameterType = "model",
            DataType = "string",
            IsRequired = false)]
        public string BatchId { get; set; }
    }

    public class DeleteBatchResponse : IHasResponseStatus
    {
        public ResponseStatus ResponseStatus { get; set; }
    }
}