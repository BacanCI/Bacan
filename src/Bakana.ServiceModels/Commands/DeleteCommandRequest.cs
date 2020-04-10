using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Commands
{
    [Tag("Command")]
    [Route("/batch/{BatchId}/step/{StepName}/command/{CommandName}", HttpMethods.Delete, Summary = "Delete Command")]
    [ApiResponse(HttpStatusCode.NotFound, "The Batch or Step or Command was not found")]
    public class DeleteCommandRequest : IReturn<DeleteCommandResponse>
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
        
        [ApiMember(
            Description = "A user-generated identifier associated with the Command",
            DataType = "string",
            ParameterType = "model",
            IsRequired = true)]
        public string CommandName { get; set; }
    }

    public class DeleteCommandResponse : IHasResponseStatus
    {
        public ResponseStatus ResponseStatus { get; set; }
    }
}