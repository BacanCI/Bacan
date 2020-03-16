using ServiceStack;

namespace Bacan.Core
{
    [Route("batch/{BatchId}/job/{Id}", "GET")]
    public class GetJob : IReturn<GetJobResponse>
    {
        public string Id { get; set; }
        public string BatchId { get; set; }
    }

    public class GetJobResponse : IHasResponseStatus
    {
        public string Id { get; set; }
        public JobState State { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
}
