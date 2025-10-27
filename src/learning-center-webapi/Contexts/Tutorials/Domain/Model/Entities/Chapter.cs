using learning_center_webapi.Contexts.Shared.Domain.Model.Entities;

namespace learning_center_webapi.Contexts.Tutorials.Domain.Model.Entities;

public class Chapter : BaseEntity
{
    private string _name = string.Empty;

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException();
            _name = value;
        }
    }

    public int Pages { get; set; }

    public int Order { get; set; }
    public string? Summary { get; set; }
    public TimeSpan? Duration { get; private set; }
    public int TutorialId { get; set; }
    public Tutorial Tutorial { get; set; } = null!;

    public void UpdateDuration(TimeSpan duration)
    {
        if (duration < TimeSpan.Zero) throw new ArgumentException();
        Duration = duration;
    }
}