namespace learning_center_webapi.Contexts.Enrolments.Domain.Model;

public class CreateEnrolmentCommand
{
    public Guid UserId { get; set; }
    public Guid TutorialId { get; set; }
    public DateTime EnrolmentDate { get; set; }
}