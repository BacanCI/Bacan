using System.Collections.Generic;
using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Steps
{
    [Tag("Step")]
    [Route("/batch/{BatchId}/step/{StepId}/artifact", HttpMethods.Post, Summary = "Create new Step Artifact")]
    [ApiResponse(HttpStatusCode.NotFound, "The Batch or Step was not found")]
    [ApiResponse(HttpStatusCode.Conflict, "The Step Artifact already exists")]
    public class CreateStepArtifactRequest : IReturn<CreateStepArtifactResponse>
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
            ParameterType = "model",
            IsRequired = true)]
        public string ArtifactId { get; set; }
        
        [ApiMember(
            Description = "A description of the Artifact",
            DataType = "string",
            ParameterType = "model")]
        public string Description { get; set; }

        [ApiMember(
            Description = "The artifact's filename",
            DataType = "string",
            ParameterType = "model",
            IsRequired = true)]
        public string FileName { get; set; }

        [ApiMember(
            Description = "An array of options associated with the Artifact",
            DataType = "string",
            ParameterType = "model")]
        public List<Option> Options { get; set; }
    }

    public class CreateStepArtifactResponse : IHasResponseStatus
    {
        public ResponseStatus ResponseStatus { get; set; }
    }
}