using System.Collections.Generic;
using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Steps
{
    [Tag("Step")]
    [Route("/batch/{BatchId}/step/{StepName}/variables", HttpMethods.Get, Summary = "Get all Step Variables")]
    [ApiResponse(HttpStatusCode.NotFound, "The Batch or Step was not found")]
    public class GetAllStepVariableRequest : IReturn<GetAllStepVariableResponse>
    {
        [ApiMember(
            Description = "A system-generated identifier associated with the Batch",
            DataType = "string",
            ParameterType = "path",
            IsRequired = true)]
        public string BatchId { get; set; }
        
        [ApiMember(
            Description = "A user-generated identifier associated with the Step",
            DataType = "string",
            ParameterType = "model",
            IsRequired = true)]
        public string StepName { get; set; }
    }

    public class GetAllStepVariableResponse : IHasResponseStatus
    {
        public List<Variable> Variables { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
}