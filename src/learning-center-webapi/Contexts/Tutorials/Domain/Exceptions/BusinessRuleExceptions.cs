namespace learning_center_webapi.Contexts.Tutorials.Domain.Exceptions;

public class DuplicateTutorialTitleException : Exception
{
    public DuplicateTutorialTitleException(string title)
        : base($"A tutorial with the title '{title}' already exists.")
    {
    }
}