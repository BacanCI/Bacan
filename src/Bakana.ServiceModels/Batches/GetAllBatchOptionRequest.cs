using System.Collections.Generic;
using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Batches
{
    [Tag("Batch")]
    [Route("/batch/{BatchId}/options", HttpMethods.Get, Summary = "Get all batch options")]
    [ApiResponse(HttpStatusCode.NotFound, "The batch or batch option was not found")]
    public class GetAllBatchOptionRequest : IReturn<GetAllBatchOptionResponse>
    {
        [ApiMember(
            Description = "A system-generated value associated with the batch",
            DataType = "string",
            ParameterType = "path",
            IsRequired = true)]
        public string BatchId { get; set; }
    }

    public class GetAllBatchOptionResponse : IHasResponseStatus
    {
        public List<Option> Options { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
}