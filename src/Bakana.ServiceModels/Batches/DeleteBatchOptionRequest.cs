using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Batches
{
    [Tag("Batch")]
    [Route("/batch/{BatchId}/option/{OptionId}", HttpMethods.Delete, Summary = "Delete batch option")]
    [ApiResponse(HttpStatusCode.NotFound, "The batch or batch option was not found")]
    public class DeleteBatchOptionRequest : IReturn<DeleteBatchOptionResponse>
    {
        [ApiMember(
            Description = "A system-generated value associated with the batch",
            DataType = "string",
            ParameterType = "path",
            IsRequired = true)]
        public string BatchId { get; set; }

        [ApiMember(
            Description = "A user-generated identifier associated with the option",
            DataType = "string",
            ParameterType = "path",
            IsRequired = true)]
        public string OptionId { get; set; }
    }

    public class DeleteBatchOptionResponse : IHasResponseStatus
    {
        public ResponseStatus ResponseStatus { get; set; }
    }
}