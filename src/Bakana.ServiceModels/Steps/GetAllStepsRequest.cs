using System.Collections.Generic;
using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Steps
{
    [Tag("Step")]
    [Route("/batch/{BatchId}/steps", HttpMethods.Get, Summary = "Get all Steps")]
    [ApiResponse(HttpStatusCode.NotFound, "The Batch was not found")]
    public class GetAllStepsRequest : IReturn<GetAllStepsResponse>
    {
        [ApiMember(
            Description = "A system-generated identifier associated with the Batch",
            DataType = "string",
            ParameterType = "path",
            IsRequired = true)]
        public string BatchId { get; set; }
    }

    public class GetAllStepsResponse : IHasResponseStatus
    {
        public List<Step> Steps { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
}