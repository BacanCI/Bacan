using System.Collections.Generic;
using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Steps
{
    [Tag("Step")]
    [Route("/batch/{BatchId}/step/{StepId}", HttpMethods.Get, Summary = "Get Step")]
    [ApiResponse(HttpStatusCode.NotFound, "The Batch or Step was not found")]
    public class GetStepRequest : IReturn<GetStepResponse>
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
    public class GetStepResponse : IHasResponseStatus
    {
        public string StepId { get; set; }

        public string Description { get; set; }

        public string[] Dependencies { get; set; }

        public string[] Tags { get; set; }

        public string[] Requirements { get; set; }

        public List<Option> Options { get; set; }

        public List<Variable> Variables { get; set; }

        public List<StepArtifact> Artifacts { get; set; }

        public List<Command> Commands { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
}