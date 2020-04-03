using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels
{
    [Tag("Batch")]
    [Route("/batch/{BatchId}", HttpMethods.Put, Summary = "Update batch")]
    [ApiResponse(HttpStatusCode.NotFound, "The batch was not found")]
    public class UpdateBatchRequest : IReturn<CreateBatchResponse>
    {
        [ApiMember(
            Description = "A system-generated value associated with the batch",
            DataType = "string",
            ParameterType = "path",
            IsRequired = true)]
        public string BatchId { get; set; }

        [ApiMember( 
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