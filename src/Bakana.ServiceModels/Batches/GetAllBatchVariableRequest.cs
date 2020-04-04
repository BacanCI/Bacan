using System.Collections.Generic;
using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Batches
{
    [Tag("Batch")]
    [Route("/batch/{BatchId}/variables", HttpMethods.Get, Summary = "Get all Batch Variables")]
    [ApiResponse(HttpStatusCode.NotFound, "The Batch was not found")]
    public class GetAllBatchVariableRequest : IReturn<GetAllBatchVariableResponse>
    {
        [ApiMember(
            Description = "A system-generated identifier associated with the Batch",
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