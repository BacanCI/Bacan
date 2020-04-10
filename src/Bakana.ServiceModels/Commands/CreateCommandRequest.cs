using System.Collections.Generic;
using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Commands
{
    [Tag("Command")]
    [Route("/batch/{BatchId}/step/{StepName}/command", HttpMethods.Post, Summary = "Create new Command")]
    [ApiResponse(HttpStatusCode.NotFound, "The Batch or Step was not found")]
    [ApiResponse(HttpStatusCode.Conflict, "The Command already exists")]
    public class CreateCommandRequest : IReturn<CreateCommandResponse>
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
            Description = "A user-generated identifier associated with the Command",
            DataType = "string",
            ParameterType = "model",
            IsRequired = true)]
        public string CommandName { get; set; }

        [ApiMember(
            Description = "A description of the Command",
            ParameterType = "model",
            DataType = "string",
            IsRequired = false)]
        public string Description { get; set; }

        [ApiMember(
            Description = "A script or process to execute",
            ParameterType = "model",
            DataType = "string",
            IsRequired = true)]
        public string Item { get; set; }

        [ApiMember(
            Description = "Options to assign to Command",
            ParameterType = "model",
            IsRequired = false)]
        public List<Option> Options { get; set; }

        [ApiMember(
            Description = "Array of variables to be used by Command",
            ParameterType = "model",
            IsRequired = false)]
        public List<Variable> Variables { get; set; }
    }

    public class CreateCommandResponse : IHasResponseStatus
    {
        public ResponseStatus ResponseStatus { get; set; }
    }
}