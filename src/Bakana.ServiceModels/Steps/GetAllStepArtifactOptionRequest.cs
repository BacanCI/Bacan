using System.Collections.Generic;
using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Steps
{
    [Tag("Step")]
    [Route("/batch/{BatchId}/step/{StepId}/artifact/{ArtifactId}/options", HttpMethods.Get, Summary = "Get all Step Artifact Options")]
    [ApiResponse(HttpStatusCode.NotFound, "The Batch or Step or Step Artifact was not found")]
    public class GetAllStepArtifactOptionRequest : IReturn<GetAllStepArtifactOptionResponse>
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
            Description = "A user-generated identifier associated with the Artifact",
            DataType = "string",
            ParameterType = "path",
            IsRequired = true)]
        public string ArtifactId { get; set; }
    }

    public class GetAllStepArtifactOptionResponse : IHasResponseStatus
    {
        public List<Option> Options { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
}