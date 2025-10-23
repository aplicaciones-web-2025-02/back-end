using learning_center_webapi.Contexts.Shared.Domain.Model.Entities;

namespace learning_center_webapi.Contexts.Tutorials.Domain.Model.Entities;

public class Chapter : BaseEntity
{
    public required string Name { get; set; }
    public int Pages { get; set; }

    public int Order { get; set; }
    public string? Summary { get; set; }
    public TimeSpan? Duration { get; set; }
    public int TutorialId { get; set; }
    public Tutorial Tutorial { get; set; } = null!;
}
