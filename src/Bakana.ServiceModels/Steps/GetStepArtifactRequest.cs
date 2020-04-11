using System.Collections.Generic;
using System.Net;
using Bakana.DomainModels;
using ServiceStack;

namespace Bakana.ServiceModels.Steps
{
    [Tag("Step")]
    [Route("/batch/{BatchId}/step/{StepName}/artifact/{ArtifactName}", HttpMethods.Get, Summary = "Get Step Artifact")]
    [ApiResponse(HttpStatusCode.NotFound, "The Batch or Step or Step Artifact was not found")]
    public class GetStepArtifactRequest : IReturn<GetStepArtifactResponse>
    {
        [ApiMember(
            Description = "A system-generated value associated with the Batch",
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
            Description = "A user-generated identifier associated with the Artifact",
            DataType = "string",
            ParameterType = "path",
            IsRequired = true)]
        public string ArtifactName { get; set; }
    }

    public class GetStepArtifactResponse : IHasResponseStatus
    {
        [ApiMember(
            Description = "A user-generated identifier associated with the Artifact")]
        public string ArtifactName { get; set; }
        
        [ApiMember(
            Description = "A description of the Artifact")]
        public string Description { get; set; }

        [ApiMember(
            Description = "The Artifact's filename")]
        public string FileName { get; set; }

        [ApiMember(
            Description = "An array of Options associated with the Artifact")]
        public List<Option> Options { get; set; }
        
        [ApiMember(
            Description = "Set to true when artifact is created by Step Worker")]
        public bool OutputArtifact { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
}