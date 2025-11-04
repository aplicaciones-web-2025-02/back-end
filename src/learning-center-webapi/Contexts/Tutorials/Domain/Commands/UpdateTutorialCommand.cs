namespace learning_center_webapi.Contexts.Tutorials.Domain.Commands;

public class UpdateTutorialCommand
{
    public  Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public DateTime PublishedDate { get; set; }

    public string Author { get; set; } = string.Empty;
    public string? AuthorEmail { get; set; }
    public int Level { get; set; }
    public int Views { get; set; }
    public string? Tags { get; set; }
}