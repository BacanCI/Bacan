using System.Collections.Generic;
using ServiceStack;

namespace Bakana.Core
{
    [Route("/batch/{Id}", "POST")]
    public class CreateBatch : IReturn<CreateBatchResponse>
    {
        public string Id { get; set; }
        public List<CreateBatchJob> Jobs { get; set; }
    }

    public class CreateBatchResponse : IHasResponseStatus
    {
        public string Id { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }

    public class CreateBatchJob : ICreateJob
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string ArchiveUrl { get; set; }
        public string ArchiveName { get; set; }
        public string ProcessName { get; set; }
        public string ProcessArguments { get; set; }
    }
}