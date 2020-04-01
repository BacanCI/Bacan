using ServiceStack;

namespace Bakana.ServiceModels
{
    public class DeleteBatchRequest : IReturn<GetBatchResponse>
    {
        [ApiMember(Name = "Id", Description = "Auto-generated batch id")]
        public string Id { get; set; }
    }

    public class DeleteBatchResponse : IHasResponseStatus
    {
        public ResponseStatus ResponseStatus { get; set; }
    }
}