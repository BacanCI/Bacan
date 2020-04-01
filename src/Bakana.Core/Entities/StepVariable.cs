using ServiceStack.DataAnnotations;

namespace Bakana.Core.Entities
{
    [UniqueConstraint(nameof(StepId), nameof(VariableId))]
    public class StepVariable : Variable
    {
        [ForeignKey(typeof(Step), OnDelete = "CASCADE", OnUpdate = "CASCADE")]
        public ulong StepId { get; set; }
    }
}