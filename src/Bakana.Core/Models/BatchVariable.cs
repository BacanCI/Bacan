using ServiceStack.DataAnnotations;

namespace Bakana.Core.Models
{
    [UniqueConstraint(nameof(BatchId), nameof(Name))]
    public class BatchVariable : Variable
    {
        [References(typeof(Batch))]
        public string BatchId { get; set; }
    }
}