using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Batches
{
    [Tag("Batch")]
    [Route("/batch/{BatchId}/variable/{VariableId}", HttpMethods.Delete, Summary = "Delete batch variable")]
    [ApiResponse(HttpStatusCode.NotFound, "The batch or batch variable was not found")]
    public class DeleteBatchVariableRequest : IReturn<DeleteBatchVariableResponse>
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

    public class DeleteBatchVariableResponse : IHasResponseStatus
    {
        public ResponseStatus ResponseStatus { get; set; }
    }
}