using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Steps
{
    [Tag("Step")]
    [Route("/batch/{BatchId}/step/{StepName}/option/{OptionName}", HttpMethods.Delete, Summary = "Delete Step Option")]
    [ApiResponse(HttpStatusCode.NotFound, "The Batch or Step or Step Option was not found")]
    public class DeleteStepOptionRequest : IReturn<DeleteStepOptionResponse>
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
            Description = "A user-generated identifier associated with the Option",
            DataType = "string",
            ParameterType = "path",
            IsRequired = true)]
        public string OptionName { get; set; }
    }

    public class DeleteStepOptionResponse : IHasResponseStatus
    {
        public ResponseStatus ResponseStatus { get; set; }
    }
}