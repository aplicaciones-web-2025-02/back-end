namespace learning_center_webapi.Contexts.Enrolments.Interfaces.REST.Resources;

public class EnrolmentResource
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid TutorialId { get; set; }
    public DateTime EnrolmentDate { get; set; }
    public double Progress { get; set; }
    public DateTime? CompletionDate { get; set; }
}