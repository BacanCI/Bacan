using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Batches
{
    [Tag("Batch")]
    [Route("/batch/{BatchId}/option/{OptionId}", HttpMethods.Put, Summary = "Update batch option")]
    [ApiResponse(HttpStatusCode.NotFound, "The batch or batch option was not found")]
    public class UpdateBatchOptionRequest : IReturn<UpdateBatchOptionResponse>
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

        [ApiMember(
            Description = "A description of the option",
            DataType = "string",
            ParameterType = "model")]
        public string Description { get; set; }

        [ApiMember(
            Description = "The value assigned to the option",
            DataType = "string",
            ParameterType = "model",
            IsRequired = true)]
        public string Value { get; set; }
    }

    public class UpdateBatchOptionResponse : IHasResponseStatus
    {
        public ResponseStatus ResponseStatus { get; set; }
    }
}