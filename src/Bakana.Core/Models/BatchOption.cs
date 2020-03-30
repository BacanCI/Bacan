using ServiceStack.DataAnnotations;

namespace Bakana.Core.Models
{
    [UniqueConstraint(nameof(BatchId), nameof(Name))]
    public class BatchOption : Option
    {
        [References(typeof(Batch))]
        public string BatchId { get; set; }
    }
}