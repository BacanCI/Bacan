using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Batches
{
    [Tag("Batch")]
    [Route("/batch/{BatchId}/option/{OptionId}", HttpMethods.Get, Summary = "Get batch option")]
    [ApiResponse(HttpStatusCode.NotFound, "The batch or batch option was not found")]
    public class GetBatchOptionRequest : IReturn<GetBatchOptionResponse>
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

    public class GetBatchOptionResponse : IHasResponseStatus
    {
        [ApiMember(
            Description = "A system-generated value associated with the batch")]
        public string BatchId { get; set; }

        [ApiMember(
            Description = "A user-generated identifier associated with the option")]
        public string OptionId { get; set; }

        [ApiMember(
            Description = "A description of the option")]
        public string Description { get; set; }

        [ApiMember(
            Description = "The value assigned to the option")]
        public string Value { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
}