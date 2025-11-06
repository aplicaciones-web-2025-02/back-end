using Microsoft.Extensions.Localization;
using learning_center_webapi.Contexts.Shared.Domain.Services;

namespace learning_center_webapi.Contexts.Tutorials.Domain.Exceptions;

public class DuplicateTutorialTitleException : Exception
{
    public string Title { get; }
    
    public DuplicateTutorialTitleException(string title)
        : base(GetLocalizedMessage("DuplicateTutorialTitle", title))
    {
        Title = title;
    }
    
    private static string GetLocalizedMessage(string key, string title)
    {
        var localizer = LocalizationService.GetLocalizer("Tutorials.TutorialController", "learning_center_webapi");
        return string.Format(localizer[key], title);
    }
}

public class TutorialNotFoundException : Exception
{
    public Guid Id { get; }
    
    public TutorialNotFoundException(Guid id)
        : base(GetLocalizedMessage("TutorialNotFoundById", id))
    {
        Id = id;
    }
    
    private static string GetLocalizedMessage(string key, Guid id)
    {
        var localizer = LocalizationService.GetLocalizer("Tutorials.TutorialController", "learning_center_webapi");
        return string.Format(localizer[key], id);
    }
}