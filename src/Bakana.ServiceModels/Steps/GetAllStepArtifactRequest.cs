using System.Collections.Generic;
using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Steps
{
    [Tag("Step")]
    [Route("/batch/{BatchId}/step/{StepName}/artifacts", HttpMethods.Get, Summary = "Get all Step Artifacts")]
    [ApiResponse(HttpStatusCode.NotFound, "The Batch or Step was not found")]
    public class GetAllStepArtifactRequest : IReturn<GetAllStepArtifactResponse>
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
    }

    public class GetAllStepArtifactResponse : IHasResponseStatus
    {
        public List<StepArtifact> Artifacts { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
}