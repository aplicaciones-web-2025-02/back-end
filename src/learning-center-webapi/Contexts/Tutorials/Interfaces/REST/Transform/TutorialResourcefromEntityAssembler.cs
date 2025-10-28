using learning_center_webapi.Contexts.Tutorials.Domain.Model.Entities;
using learning_center_webapi.Contexts.Tutorials.Interfaces.REST.Resources;

namespace learning_center_webapi.Contexts.Tutorials.Interfaces.REST.Transform;

public static class TutorialResourceFromEntityAssembler
{
    public static TutorialResource ToResource(Tutorial tutorial)
    {
        var chapters = new List<ChapterResource>();


        foreach (var tutorialChapter in tutorial.Chapters)
            chapters.Add(new ChapterResource
            {
                ChapterName = tutorialChapter.Name
            });

        return new TutorialResource
        {
            Title = tutorial.Title,
            Description = tutorial.Description,
            Author = tutorial.Author,
            PublishedDate = tutorial.PublishedDate,
            AuthorEmail = tutorial.AuthorEmail,
            Level = tutorial.Level,
            Views = tutorial.Views,
            Tags = tutorial.Tags,
            chapters = chapters
        };
    }
}