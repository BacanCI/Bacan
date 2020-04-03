using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels
{
    [Tag("Batch")]
    [Route("/batch/{BatchId}", HttpMethods.Delete, Summary = "Delete batch")]
    [ApiResponse(HttpStatusCode.NotFound, "The batch was not found")]
    public class DeleteBatchRequest : IReturn<GetBatchResponse>
    {
        [ApiMember(
            Description = "A system-generated value associated with the batch",
            DataType = "string",
            ParameterType = "path",
            IsRequired = true)]
        public string BatchId { get; set; }
    }

    public class DeleteBatchResponse : IHasResponseStatus
    {
        public ResponseStatus ResponseStatus { get; set; }
    }
}