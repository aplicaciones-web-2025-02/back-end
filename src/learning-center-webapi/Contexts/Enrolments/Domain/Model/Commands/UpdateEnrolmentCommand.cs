namespace learning_center_webapi.Contexts.Enrolments.Domain.Model;

public class UpdateEnrolmentCommand
{
    public DateTime? CompletionDate { get; set; }
    public double? Progress { get; set; }
}