using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Batches
{
    [Tag("Step")]
    [Route("/batch/{BatchId}/step/{StepId}/variable/{VariableId}", HttpMethods.Delete, Summary = "Delete Step Variable")]
    [ApiResponse(HttpStatusCode.NotFound, "The Batch or Step or Step Variable was not found")]
    public class DeleteStepVariableRequest : IReturn<DeleteStepVariableResponse>
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

    public class DeleteStepVariableResponse : IHasResponseStatus
    {
        public ResponseStatus ResponseStatus { get; set; }
    }
}