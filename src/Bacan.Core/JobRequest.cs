using ServiceStack;

namespace Bacan.Core
{
    [Route("/job")]
    public class JobRequest : IReturn<JobResponse>
    {
        public string JobId { get; set; }
        public string GroupId { get; set; }
        public string Description { get; set; }
    }
    
    public class JobResponse
    {
        public string JobId { get; set; }
        public string Result { get; set; }
    }
}