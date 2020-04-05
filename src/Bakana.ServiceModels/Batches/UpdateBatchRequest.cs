using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Batches
{
    [Tag("Batch")]
    [Route("/batch/{BatchId}", HttpMethods.Put, Summary = "Update Batch")]
    [ApiResponse(HttpStatusCode.NotFound, "The Batch was not found")]
    public class UpdateBatchRequest : IReturn<UpdateBatchResponse>
    {
        [ApiMember(
            Description = "A system-generated identifier associated with the Batch",
            DataType = "string",
            ParameterType = "path",
            IsRequired = true)]
        public string BatchId { get; set; }

        [ApiMember( 
            Description = "A description of the Batch",
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