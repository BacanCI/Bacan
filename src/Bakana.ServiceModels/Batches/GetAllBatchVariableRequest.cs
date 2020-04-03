using System.Collections.Generic;
using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Batches
{
    [Tag("Batch")]
    [Route("/batch/{BatchId}/variables", HttpMethods.Get, Summary = "Get all batch Variables")]
    [ApiResponse(HttpStatusCode.NotFound, "The batch or batch Variable was not found")]
    public class GetAllBatchVariableRequest : IReturn<GetAllBatchVariableResponse>
    {
        [ApiMember(
            Description = "A system-generated value associated with the batch",
            DataType = "string",
            ParameterType = "path",
            IsRequired = true)]
        public string BatchId { get; set; }
    }

    public class GetAllBatchVariableResponse : IHasResponseStatus
    {
        public List<Variable> Variables { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
}