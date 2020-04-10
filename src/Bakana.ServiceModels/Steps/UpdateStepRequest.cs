using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Steps
{
    [Tag("Step")]
    [Route("/batch/{BatchId}/step/{StepName}", HttpMethods.Put, Summary = "Update Step")]
    [ApiResponse(HttpStatusCode.NotFound, "The Batch or Step was not found")]
    public class UpdateStepRequest : IReturn<UpdateStepResponse>
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
            Description = "A description of the Step",
            ParameterType = "model", 
            DataType = "string", 
            IsRequired = false)]
        public string Description { get; set; }

        public string[] Dependencies { get; set; }

        public string[] Tags { get; set; }

        public string[] Requirements { get; set; }
    }

    public class UpdateStepResponse : IHasResponseStatus
    {
        public ResponseStatus ResponseStatus { get; set; }
    }
}