using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Batches
{
    [Tag("Batch")]
    [Route("/batch/{BatchId}/variable/{VariableId}", HttpMethods.Get, Summary = "Get Batch Variable")]
    [ApiResponse(HttpStatusCode.NotFound, "The Batch or Batch Variable was not found")]
    public class GetBatchVariableRequest : IReturn<GetBatchVariableResponse>
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
        public string VariableId { get; set; }
        
    }

    public class GetBatchVariableResponse : IHasResponseStatus
    {
        [ApiMember(
            Description = "A user-generated identifier associated with the Variable")]
        public string VariableId { get; set; }

        [ApiMember(
            Description = "A description of the Variable")]
        public string Description { get; set; }

        [ApiMember(
            Description = "The value assigned to the Variable")]
        public string Value { get; set; }

        [ApiMember(
            Description = "Set to true if the Variable is considered sensitive or secure")]
        public bool Sensitive { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
}