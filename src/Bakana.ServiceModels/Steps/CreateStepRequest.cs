using System.Collections.Generic;
using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Steps
{
    [Tag("Step")]
    [Route("/batch/{BatchId}/step", HttpMethods.Post, Summary = "Create new Step")]
    [ApiResponse(HttpStatusCode.NotFound, "The Batch was not found")]
    [ApiResponse(HttpStatusCode.Conflict, "The Step already exists")]
    public class CreateStepRequest : IReturn<CreateStepResponse>
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
            Description = "A description of the Step",
            ParameterType = "model",
            DataType = "string",
            IsRequired = false)]
        public string Description { get; set; }

        [ApiMember(
            Description = "Array of Dependent Step Ids",
            ParameterType = "model",
            IsRequired = false)]
        public string[] Dependencies { get; set; }

        [ApiMember(
            Description = "Array of tag values",
            ParameterType = "model",
            IsRequired = false)]
        public string[] Tags { get; set; }

        [ApiMember(
            Description = "Array of Step Worker requirement Id values",
            ParameterType = "model",
            IsRequired = false)]
        public string[] Requirements { get; set; }

        [ApiMember(
            Description = "Options to assign to Step",
            ParameterType = "model",
            IsRequired = false)]
        public List<Option> Options { get; set; }

        [ApiMember(
            Description = "Array of variables to be used by all steps in the Step",
            ParameterType = "model",
            IsRequired = false)]
        public List<Variable> Variables { get; set; }

        [ApiMember(
            Description = "An array of artifacts associated with Step",
            ParameterType = "model",
            IsRequired = false)]
        public List<StepArtifact> Artifacts { get; set; }
        
        [ApiMember(
            Description = "An array of commands associated with Step",
            ParameterType = "model",
            IsRequired = false)]
        public List<Command> Commands { get; set; }
    }

    public class CreateStepResponse : IHasResponseStatus
    {
        public ResponseStatus ResponseStatus { get; set; }
    }
}