using System.Collections.Generic;
using ServiceStack;

namespace Bakana.Core
{
    [Route("/batch/{Id}", "GET")]
    public class GetBatch : IReturn<GetBatchResponse>
    {
        public string Id { get; set; }
    }

    public class GetBatchResponse : IHasResponseStatus
    {
        public string Id { get; set; }
        public List<GetJob> Jobs { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }
}