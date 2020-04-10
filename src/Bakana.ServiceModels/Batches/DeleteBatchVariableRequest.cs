using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Batches
{
    [Tag("Batch")]
    [Route("/batch/{BatchId}/variable/{VariableName}", HttpMethods.Delete, Summary = "Delete Batch Variable")]
    [ApiResponse(HttpStatusCode.NotFound, "The Batch or Batch Variable was not found")]
    public class DeleteBatchVariableRequest : IReturn<DeleteBatchVariableResponse>
    {
        [ApiMember(
            Description = "A system-generated identifier associated with the Batch",
            DataType = "string",
            ParameterType = "path",
            IsRequired = true)]
        public string BatchId { get; set; }

        [ApiMember(
            Description = "A user-generated identifier associated with the Variable",
            DataType = "string",
            ParameterType = "path",
            IsRequired = true)]
        public string VariableName { get; set; }
    }

    public class DeleteBatchVariableResponse : IHasResponseStatus
    {
        public ResponseStatus ResponseStatus { get; set; }
    }
}