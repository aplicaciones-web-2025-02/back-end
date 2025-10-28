namespace learning_center_webapi.Contexts.Tutorials.Domain.Model.Queries;

public class GetByidTutorial
{
    public GetByidTutorial(int tutorialId)
    {
        Id = tutorialId;
    }

    public int Id { get; set; }
}