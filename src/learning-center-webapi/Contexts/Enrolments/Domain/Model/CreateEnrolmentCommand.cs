namespace learning_center_webapi.Contexts.Enrolments.Domain.Model;

public class CreateEnrolmentCommand
{
    public int UserId { get; set; }
    public int TutorialId { get; set; }
    public DateTime EnrolmentDate { get; set; }
}