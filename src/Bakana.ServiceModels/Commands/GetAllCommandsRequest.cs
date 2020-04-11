using System.Collections.Generic;
using System.Net;
using Bakana.DomainModels;
using Bakana.ServiceModels.Steps;
using ServiceStack;

namespace Bakana.ServiceModels.Commands
{
    [Tag("Command")]
    [Route("/batch/{BatchId}/step/{StepName}/commands", HttpMethods.Get, Summary = "Get all Commands")]
    [ApiResponse(HttpStatusCode.NotFound, "The Batch or Step was not found")]
    public class GetAllCommandsRequest : IReturn<GetAllStepsResponse>
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

    public class GetAllCommandsResponse : IHasResponseStatus
    {
        public List<Command> Commands { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
}