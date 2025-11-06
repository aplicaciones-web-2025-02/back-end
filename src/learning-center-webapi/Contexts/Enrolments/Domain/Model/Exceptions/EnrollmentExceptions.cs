namespace learning_center_webapi.Contexts.Enrolments.Domain.Model.Exceptions;

public class TutorialNotExistExceptions : Exception
{
    public TutorialNotExistExceptions(Guid id)
        : base($"A tutorial with the Id '{id}' doesn't exists.")
    {
    }
}
public class UserNotExistExceptions : Exception
{
    public UserNotExistExceptions(Guid id)
        : base($"A user with the Id '{id}' doesn't exists.")
    {
    }
}