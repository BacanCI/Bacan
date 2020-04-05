using System.Net;
using ServiceStack;

namespace Bakana.ServiceModels.Steps
{
    [Tag("Step")]
    [Route("/batch/{BatchId}/step/{StepId}/option/{OptionId}", HttpMethods.Get, Summary = "Get Step Option")]
    [ApiResponse(HttpStatusCode.NotFound, "The Batch or Step or Step Option was not found")]
    public class GetStepOptionRequest : IReturn<GetStepOptionResponse>
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
            Description = "A user-generated identifier associated with the Option",
            DataType = "string",
            ParameterType = "path",
            IsRequired = true)]
        public string OptionId { get; set; }
    }

    public class GetStepOptionResponse : IHasResponseStatus
    {
        [ApiMember(
            Description = "A user-generated identifier associated with the Option")]
        public string OptionId { get; set; }

        [ApiMember(
            Description = "A description of the Option")]
        public string Description { get; set; }

        [ApiMember(
            Description = "The value assigned to the Option")]
        public string Value { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
}