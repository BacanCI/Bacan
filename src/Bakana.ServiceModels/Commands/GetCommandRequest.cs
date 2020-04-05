using System.Collections.Generic;
using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Commands
{
    [Tag("Command")]
    [Route("/batch/{BatchId}/step/{StepId}/command/{CommandId}", HttpMethods.Get, Summary = "Get Command")]
    [ApiResponse(HttpStatusCode.NotFound, "The Batch or Step or Command was not found")]
    public class GetCommandRequest : IReturn<GetCommandResponse>
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

    public class GetCommandResponse : IHasResponseStatus
    {
        public string CommandId { get; set; }

        public string Description { get; set; }

        public string Item { get; set; }

        public List<Option> Options { get; set; }

        public List<Variable> Variables { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
}