using ServiceStack.DataAnnotations;

namespace Bakana.Core.Entities
{
    [UniqueConstraint(nameof(BatchId), nameof(Name))]
    public class BatchVariable : Variable
    {
        [References(typeof(Batch))]
        public string BatchId { get; set; }
    }
}