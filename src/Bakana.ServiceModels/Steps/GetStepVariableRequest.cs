using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Steps
{
    [Tag("Step")]
    [Route("/batch/{BatchId}/step/{StepId}/variable/{VariableId}", HttpMethods.Get, Summary = "Get Step Variable")]
    [ApiResponse(HttpStatusCode.NotFound, "The Batch or Step or Step Variable was not found")]
    public class GetStepVariableRequest : IReturn<GetStepVariableResponse>
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
            Description = "A user-generated identifier associated with the Variable",
            DataType = "string",
            ParameterType = "path",
            IsRequired = true)]
        public string VariableId { get; set; }
    }

    public class GetStepVariableResponse : IHasResponseStatus
    {
        [ApiMember(
            Description = "A user-generated identifier associated with the Variable")]
        public string VariableId { get; set; }

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