using learning_center_webapi.Contexts.Shared.Domain.Model.Entities;
using learning_center_webapi.Contexts.Tutorials.Domain.Model.Entities;

namespace learning_center_webapi.Contexts.Enrolments.Domain.Model.Aggregate;

public class Enrolment : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid TutorialId { get; set; }
    public DateTime EnrolmentDate { get; set; } = DateTime.UtcNow;
    public double Progress { get; set; } = 0.0;
    public DateTime? CompletionDate { get; set; }
    public Tutorial Tutorial { get; set; } = null!;
}