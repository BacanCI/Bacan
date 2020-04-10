using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Commands
{
    [Tag("Command")]
    [Route("/batch/{BatchId}/step/{StepName}/command/{CommandName}/option/{OptionName}", HttpMethods.Get, Summary = "Get Command Option")]
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
        public string StepName { get; set; }

        [ApiMember(
            Description = "A user-generated identifier associated with the Command",
            DataType = "string",
            ParameterType = "model",
            IsRequired = true)]
        public string CommandName { get; set; }

        [ApiMember(
            Description = "A user-generated identifier associated with the Option",
            DataType = "string",
            ParameterType = "path",
            IsRequired = true)]
        public string OptionName { get; set; }
    }

    public class GetCommandOptionResponse : IHasResponseStatus
    {
        [ApiMember(
            Description = "A user-generated identifier associated with the Option")]
        public string OptionName { get; set; }

        [ApiMember(
            Description = "A description of the Option")]
        public string Description { get; set; }

        [ApiMember(
            Description = "The value assigned to the Option")]
        public string Value { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
}