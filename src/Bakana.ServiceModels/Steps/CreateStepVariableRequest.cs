using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Steps
{
    [Tag("Step")]
    [Route("/batch/{BatchId}/step/{StepId}/variable", HttpMethods.Post, Summary = "Create new Step Variable")]
    [ApiResponse(HttpStatusCode.NotFound, "The Batch or Step was not found")]
    [ApiResponse(HttpStatusCode.Conflict, "The Step Variable already exists")]
    public class CreateStepVariableRequest : IReturn<CreateStepVariableResponse>
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
            ParameterType = "model",
            IsRequired = true)]
        public string VariableId { get; set; }

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

    public class CreateStepVariableResponse : IHasResponseStatus
    {
        public ResponseStatus ResponseStatus { get; set; }
    }
}