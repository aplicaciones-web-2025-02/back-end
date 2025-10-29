namespace learning_center_webapi.Contexts.Enrolments.Interfaces.REST.Resources;

public class EnrolmentResource
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int TutorialId { get; set; }
    public DateTime EnrolmentDate { get; set; }
    public double Progress { get; set; }
    public DateTime? CompletionDate { get; set; }
}