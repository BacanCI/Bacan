using System.Collections.Generic;
using System.Linq;
using Bacan.Core;

namespace Bacan.Batching
{
    public interface IBatch
    {
        void Add(IJob job);
        void Update(string id, JobState state);
        void Clear();
        
        bool IsCompleted { get; }
    }

    public class Batch : IBatch
    {
        private readonly Dictionary<string, IJob> jobs;
        
        public Batch()
        {
            jobs = new Dictionary<string, IJob>();
        }

        public void Add(IJob job)
        {
            jobs.Add(job.Id, job);
        }
        
        public void Update(string id, JobState state)
        {
            var job = jobs[id].State == state;
        }

        public void Clear()
        {
            jobs.Clear();
        }
        
        public bool IsCompleted => jobs.Any() && jobs.All(j => j.Value.State == JobState.Completed);
    }
}