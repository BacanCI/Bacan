using ServiceStack.DataAnnotations;

namespace Bakana.Core.Entities
{
    [UniqueConstraint(nameof(BatchId), nameof(Name))]
    public class BatchVariable : Variable
    {
        [ForeignKey(typeof(Batch), OnDelete = "CASCADE", OnUpdate = "CASCADE")]
        public string BatchId { get; set; }
    }
}