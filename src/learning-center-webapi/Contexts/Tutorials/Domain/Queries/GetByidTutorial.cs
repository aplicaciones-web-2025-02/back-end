namespace learning_center_webapi.Contexts.Tutorials.Domain.Model.Queries;

public class GetByidTutorial
{
    public GetByidTutorial(Guid tutorialId)
    {
        Id = tutorialId;
    }

    public Guid Id { get; set; }
}