using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Commands
{
    [Tag("Command")]
    [Route("/batch/{BatchId}/step/{StepId}/command/{CommandId}", HttpMethods.Put, Summary = "Update Command")]
    [ApiResponse(HttpStatusCode.NotFound, "The Batch or Step or Command was not found")]
    public class UpdateCommandRequest : IReturn<UpdateCommandResponse>
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

        [ApiMember( 
            Description = "A description of the Command",
            ParameterType = "model", 
            DataType = "string", 
            IsRequired = false)]
        public string Description { get; set; }

        [ApiMember(
            Description = "A script or process to execute",
            ParameterType = "model",
            DataType = "string",
            IsRequired = true)]
        public string Item { get; set; }
    }

    public class UpdateCommandResponse : IHasResponseStatus
    {
        public ResponseStatus ResponseStatus { get; set; }
    }
}