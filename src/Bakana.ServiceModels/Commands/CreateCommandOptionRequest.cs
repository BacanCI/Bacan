using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Commands
{
    [Tag("Command")]
    [Route("/batch/{BatchId}/step/{StepId}/command/{CommandId}/option", HttpMethods.Post, Summary = "Create new Command Option")]
    [ApiResponse(HttpStatusCode.NotFound, "The Batch or Step or Command was not found")]
    [ApiResponse(HttpStatusCode.Conflict, "The Command Option already exists")]
    public class CreateCommandOptionRequest : IReturn<CreateCommandOptionResponse>
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
            ParameterType = "model",
            IsRequired = true)]
        public string OptionId { get; set; }

        [ApiMember(
            Description = "A description of the Option",
            DataType = "string",
            ParameterType = "model")]
        public string Description { get; set; }

        [ApiMember(
            Description = "The value assigned to the Option",
            DataType = "string",
            ParameterType = "model",
            IsRequired = true)]
        public string Value { get; set; }
    }

    public class CreateCommandOptionResponse : IHasResponseStatus
    {
        public ResponseStatus ResponseStatus { get; set; }
    }
}