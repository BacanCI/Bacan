using ServiceStack.DataAnnotations;

namespace Bakana.Core.Entities
{
    [UniqueConstraint(nameof(StepId), nameof(OptionId))]
    public class StepOption : Option
    {
        [ForeignKey(typeof(Step), OnDelete = "CASCADE", OnUpdate = "CASCADE")]
        public ulong StepId { get; set; }
    }
}