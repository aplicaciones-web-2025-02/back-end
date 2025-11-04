namespace learning_center_webapi.Contexts.Tutorials.Domain.Commands;

public class UpdateAuthorTutorialCommand
{
    public int Id { get; set; }
    public string Author { get; set; } = string.Empty;
}