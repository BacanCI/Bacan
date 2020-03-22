using ServiceStack;

namespace Bakana.Core
{
    [Route("batch/{BatchId}/job/{Id}", "POST")]
    public class CreateJob : IReturn<CreateJobResponse>, ICreateJob
    {
        public string Id { get; set; }
        public string BatchId { get; set; }
        public string Description { get; set; }
        public string ArchiveUrl { get; set; }
        public string ArchiveName { get; set; }
        public string ProcessName { get; set; }
        public string ProcessArguments { get; set; }
    }

    public class CreateJobResponse : IHasResponseStatus
    {
        public string Id { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }
}
