using learning_center_webapi.Contexts.Shared.Domain.Services;

namespace learning_center_webapi.Contexts.Enrolments.Domain.Model.Exceptions;

public class TutorialNotExistExceptions : Exception
{
    public TutorialNotExistExceptions(Guid id)
        : base(GetLocalizedMessage("TutorialNotExist", id))
    {
    }
    
    private static string GetLocalizedMessage(string key, Guid id)
    {
        var localizer = LocalizationService.GetLocalizer("Enrolments.Enrolment", "learning_center_webapi");
        return string.Format(localizer[key], id);
    }
}

public class UserNotExistExceptions : Exception
{
    public UserNotExistExceptions(Guid id)
        : base(GetLocalizedMessage("UserNotExist", id))
    {
    }
    
    private static string GetLocalizedMessage(string key, Guid id)
    {
        var localizer = LocalizationService.GetLocalizer("Enrolments.Enrolment", "learning_center_webapi");
        return string.Format(localizer[key], id);
    }
}