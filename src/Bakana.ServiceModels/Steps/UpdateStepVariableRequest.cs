using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Steps
{
    [Tag("Step")]
    [Route("/batch/{BatchId}/step/{StepName}/variable/{VariableName}", HttpMethods.Put, Summary = "Update Step Variable")]
    [ApiResponse(HttpStatusCode.NotFound, "The Batch or Step or Step Variable was not found")]
    public class UpdateStepVariableRequest : IReturn<UpdateStepVariableResponse>
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
            Description = "A user-generated identifier associated with the Variable",
            DataType = "string",
            ParameterType = "path",
            IsRequired = true)]
        public string VariableName { get; set; }

        [ApiMember(
            Description = "A description of the Variable",
            DataType = "string",
            ParameterType = "model")]
        public string Description { get; set; }

        [ApiMember(
            Description = "The value assigned to the Variable",
            DataType = "string",
            ParameterType = "model",
            IsRequired = true)]
        public string Value { get; set; }

        [ApiMember(
            Description = "Set to true if the Variable is considered sensitive or secure",
            DataType = "bool",
            ParameterType = "model")]
        public bool Sensitive { get; set; }
    }

    public class UpdateStepVariableResponse : IHasResponseStatus
    {
        public ResponseStatus ResponseStatus { get; set; }
    }
}