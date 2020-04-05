using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Commands
{
    [Tag("Command")]
    [Route("/batch/{BatchId}/step/{StepId}/command/{CommandId}/option/{OptionId}", HttpMethods.Get, Summary = "Get Command Option")]
    [ApiResponse(HttpStatusCode.NotFound, "The Batch or Step or Command or Command Option was not found")]
    public class GetCommandOptionRequest : IReturn<GetCommandOptionResponse>
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

    public class GetCommandOptionResponse : IHasResponseStatus
    {
        [ApiMember(
            Description = "A user-generated identifier associated with the Option")]
        public string OptionId { get; set; }

        [ApiMember(
            Description = "A description of the Option")]
        public string Description { get; set; }

        [ApiMember(
            Description = "The value assigned to the Option")]
        public string Value { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
}