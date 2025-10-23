using System;
using learning_center_webapi.Contexts.Shared.Domain.Model.Entities;
using learning_center_webapi.Contexts.Tutorials.Domain.Model.ValueObjects;

namespace learning_center_webapi.Contexts.Tutorials.Domain.Model.Entities;

public class Tutorial : BaseEntity
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public DateTime PublishedDate { get; set; }

    public string Author { get; set; } = string.Empty;
    public int Level { get; set; }
    public bool IsPublished { get; set; }
    public int Views { get; set; }
    public string? Tags { get; set; }
    public ICollection<Chapter> Chapters { get; set; } = new List<Chapter>();
    public Duration Duration
    {
        get
        {
            var total = Chapters?.Where(c => c.Duration.HasValue)
                .Select(c => c.Duration!.Value)
                .Aggregate(TimeSpan.Zero, (acc, d) => acc + d) ?? TimeSpan.Zero;
            return new Duration(total);
        }
    }
}
