using System.Collections.Generic;
using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Batches
{
    [Tag("Batch")]
    [Route("/batch/{BatchId}/options", HttpMethods.Get, Summary = "Get all Batch Options")]
    [ApiResponse(HttpStatusCode.NotFound, "The Batch was not found")]
    public class GetAllBatchOptionRequest : IReturn<GetAllBatchOptionResponse>
    {
        [ApiMember(
            Description = "A system-generated identifier associated with the Batch",
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