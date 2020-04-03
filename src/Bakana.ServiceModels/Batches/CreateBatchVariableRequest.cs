using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Batches
{
    [Tag("Batch")]
    [Route("/batch/{BatchId}/variable", HttpMethods.Post, Summary = "Create new batch variable")]
    [ApiResponse(HttpStatusCode.NotFound, "The batch was not found")]
    [ApiResponse(HttpStatusCode.Conflict, "The variable already exists")]
    public class CreateBatchVariableRequest : IReturn<CreateBatchVariableResponse>
    {
        [ApiMember(
            Description = "A system-generated value associated with the batch",
            DataType = "string",
            ParameterType = "path",
            IsRequired = true)]
        public string BatchId { get; set; }

        [ApiMember(
            Description = "A user-generated identifier associated with the variable",
            DataType = "string",
            ParameterType = "model")]
        public string VariableId { get; set; }

        [ApiMember(
            Description = "A description of the variable",
            DataType = "string",
            ParameterType = "model")]
        public string Description { get; set; }

        [ApiMember(
            Description = "The value assigned to the variable",
            DataType = "string",
            ParameterType = "model",
            IsRequired = true)]
        public string Value { get; set; }

        [ApiMember(
            Description = "Set to true if the variable is considered sensitive or secure",
            DataType = "bool",
            ParameterType = "model")]
        public bool Sensitive { get; set; }
    }

    public class CreateBatchVariableResponse : IHasResponseStatus
    {
        public ResponseStatus ResponseStatus { get; set; }
    }
}