using System.Collections.Generic;
using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Commands
{
    [Tag("Command")]
    [Route("/batch/{BatchId}/step/{StepId}/command/{CommandId}/options", HttpMethods.Get, Summary = "Get all Command Options")]
    [ApiResponse(HttpStatusCode.NotFound, "The Batch or Step or Command was not found")]
    public class GetAllCommandOptionRequest : IReturn<GetAllCommandOptionResponse>
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
        
        [ApiMember(
            Description = "A user-generated identifier associated with the Command",
            DataType = "string",
            ParameterType = "model",
            IsRequired = true)]
        public string CommandId { get; set; }
    }

    public class GetAllCommandOptionResponse : IHasResponseStatus
    {
        public List<Option> Options { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
}