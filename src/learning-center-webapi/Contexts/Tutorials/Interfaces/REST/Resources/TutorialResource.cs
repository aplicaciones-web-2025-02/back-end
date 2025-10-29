namespace learning_center_webapi.Contexts.Tutorials.Interfaces.REST.Resources;

public class TutorialResource
{
    public required string Title { get; set; }
    public required string Description { get; set; }

    public DateTime PublishedDate { get; set; }

    public string Author { get; set; } = string.Empty;
    public string? AuthorEmail { get; set; }
    public int Level { get; set; }
    public bool IsPublished { get; }
    public int Views { get; set; }
    public string? Tags { get; set; }

    public List<ChapterResource> chapters { get; set; } = new();
}