using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Commands
{
    [Tag("Command")]
    [Route("/batch/{BatchId}/step/{StepName}/command/{CommandName}/variable/{VariableName}", HttpMethods.Get, Summary = "Get Command Variable")]
    [ApiResponse(HttpStatusCode.NotFound, "The Batch or Step or Command or Command Variable was not found")]
    public class GetCommandVariableRequest : IReturn<GetCommandVariableResponse>
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
            Description = "A user-generated identifier associated with the Variable",
            DataType = "string",
            ParameterType = "path",
            IsRequired = true)]
        public string VariableName { get; set; }
    }

    public class GetCommandVariableResponse : IHasResponseStatus
    {
        [ApiMember(
            Description = "A user-generated identifier associated with the Variable")]
        public string VariableName { get; set; }

        [ApiMember(
            Description = "A description of the Variable")]
        public string Description { get; set; }

        [ApiMember(
            Description = "The value assigned to the Variable")]
        public string Value { get; set; }

        [ApiMember(
            Description = "Set to true if the Variable is considered sensitive or secure")]
        public bool Sensitive { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
}