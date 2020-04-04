using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Batches
{
    [Tag("Batch")]
    [Route("/batch/{BatchId}/option/{OptionId}", HttpMethods.Get, Summary = "Get Batch Option")]
    [ApiResponse(HttpStatusCode.NotFound, "The Batch or Batch Option was not found")]
    public class GetBatchOptionRequest : IReturn<GetBatchOptionResponse>
    {
        [ApiMember(
            Description = "A system-generated identifier associated with the Batch",
            DataType = "string",
            ParameterType = "path",
            IsRequired = true)]
        public string BatchId { get; set; }

        [ApiMember(
            Description = "A user-generated identifier associated with the Option",
            DataType = "string",
            ParameterType = "path",
            IsRequired = true)]
        public string OptionId { get; set; }
    }

    public class GetBatchOptionResponse : IHasResponseStatus
    {
        [ApiMember(
            Description = "A user-generated identifier associated with the Option")]
        public string OptionId { get; set; }

        [ApiMember(
            Description = "A description of the Option")]
        public string Description { get; set; }

        [ApiMember(
            Description = "The value assigned to the Option")]
        public string Value { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
}