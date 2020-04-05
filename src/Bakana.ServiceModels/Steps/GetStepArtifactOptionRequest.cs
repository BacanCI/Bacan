using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Steps
{
    [Tag("Step")]
    [Route("/batch/{BatchId}/step/{StepId}/artifact/{ArtifactId}/option/{OptionId}", HttpMethods.Get, Summary = "Get Step Artifact Option")]
    [ApiResponse(HttpStatusCode.NotFound, "The Batch or Step or Step Artifact or Step Artifact Option was not found")]
    public class GetStepArtifactOptionRequest : IReturn<GetStepArtifactOptionResponse>
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
        public string StepId { get; set; }

        [ApiMember(
            Description = "A user-generated identifier associated with the Artifact",
            DataType = "string",
            ParameterType = "path",
            IsRequired = true)]
        public string ArtifactId { get; set; }
        
        [ApiMember(
            Description = "A user-generated identifier associated with the Artifact Option",
            DataType = "string",
            ParameterType = "path",
            IsRequired = true)]
        public string OptionId { get; set; }
    }

    public class GetStepArtifactOptionResponse : IHasResponseStatus
    {
        [ApiMember(
            Description = "A user-generated identifier associated with the Artifact Option")]
        public string OptionId { get; set; }
        
        [ApiMember(
            Description = "A description of the Artifact Option")]
        public string Description { get; set; }

        [ApiMember(
            Description = "The value assigned to the Artifact Option")]
        public string Value { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
}