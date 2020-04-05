using System.Collections.Generic;
using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Steps
{
    [Tag("Step")]
    [Route("/batch/{BatchId}/step/{StepId}/options", HttpMethods.Get, Summary = "Get all Step Options")]
    [ApiResponse(HttpStatusCode.NotFound, "The Batch or Step was not found")]
    public class GetAllStepOptionRequest : IReturn<GetAllStepOptionResponse>
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
        public string StepId { get; set; }
    }

    public class GetAllStepOptionResponse : IHasResponseStatus
    {
        public List<Option> Options { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
}