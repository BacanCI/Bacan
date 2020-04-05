using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Steps
{
    [Tag("Step")]
    [Route("/batch/{BatchId}/step/{StepId}", HttpMethods.Delete, Summary = "Delete Step")]
    [ApiResponse(HttpStatusCode.NotFound, "The Batch or Step was not found")]
    public class DeleteStepRequest : IReturn<DeleteStepResponse>
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
    }

    public class DeleteStepResponse : IHasResponseStatus
    {
        public ResponseStatus ResponseStatus { get; set; }
    }
}