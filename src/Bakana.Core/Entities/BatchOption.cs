using ServiceStack.DataAnnotations;

namespace Bakana.Core.Entities
{
    [UniqueConstraint(nameof(BatchId), nameof(OptionId))]
    public class BatchOption : Option
    {
        [ForeignKey(typeof(Batch), OnDelete = "CASCADE", OnUpdate = "CASCADE")]
        public string BatchId { get; set; }
    }
}