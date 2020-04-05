using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Commands
{
    [Tag("Command")]
    [Route("/batch/{BatchId}/step/{StepId}/command/{CommandId}/option/{OptionId}", HttpMethods.Delete, Summary = "Delete Command Option")]
    [ApiResponse(HttpStatusCode.NotFound, "The Batch or Step or Command or Command Option was not found")]
    public class DeleteCommandOptionRequest : IReturn<DeleteCommandOptionResponse>
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
            Description = "A user-generated identifier associated with the Option",
            DataType = "string",
            ParameterType = "path",
            IsRequired = true)]
        public string OptionId { get; set; }
    }

    public class DeleteCommandOptionResponse : IHasResponseStatus
    {
        public ResponseStatus ResponseStatus { get; set; }
    }
}