using System.Collections.Generic;
using System.Net;
using Bakana.DomainModels;
using ServiceStack;

namespace Bakana.ServiceModels.Batches
{
    [Tag("Batch")]
    [Route("/batch/{BatchId}/artifact", HttpMethods.Post, Summary = "Create new Batch Artifact")]
    [ApiResponse(HttpStatusCode.NotFound, "The Batch was not found")]
    [ApiResponse(HttpStatusCode.Conflict, "The Batch Artifact already exists")]
    public class CreateBatchArtifactRequest : IReturn<CreateBatchArtifactResponse>
    {
        [ApiMember(
            Description = "A system-generated identifier associated with the Batch",
            DataType = "string",
            ParameterType = "path",
            IsRequired = true)]
        public string BatchId { get; set; }

        [ApiMember(
            Description = "A user-generated identifier associated with the Artifact",
            DataType = "string",
            ParameterType = "model",
            IsRequired = true)]
        public string ArtifactName { get; set; }
        
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

    public class CreateBatchArtifactResponse : IHasResponseStatus
    {
        public ResponseStatus ResponseStatus { get; set; }
    }
}