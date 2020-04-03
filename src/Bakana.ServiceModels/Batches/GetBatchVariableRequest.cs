using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Batches
{
    [Tag("Batch")]
    [Route("/batch/{BatchId}/variable/{VariableId}", HttpMethods.Get, Summary = "Get batch variable")]
    [ApiResponse(HttpStatusCode.NotFound, "The batch or batch variable was not found")]
    public class GetBatchVariableRequest : IReturn<GetBatchVariableResponse>
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
            ParameterType = "path",
            IsRequired = true)]
        public string VariableId { get; set; }
        
    }

    public class GetBatchVariableResponse : IHasResponseStatus
    {
        [ApiMember(
            Description = "A system-generated value associated with the batch")]
        public string BatchId { get; set; }

        [ApiMember(
            Description = "A user-generated identifier associated with the variable")]
        public string VariableId { get; set; }

        [ApiMember(
            Description = "A description of the variable")]
        public string Description { get; set; }

        [ApiMember(
            Description = "The value assigned to the variable")]
        public string Value { get; set; }

        [ApiMember(
            Description = "Set to true if the variable is considered sensitive or secure")]
        public bool Sensitive { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
}