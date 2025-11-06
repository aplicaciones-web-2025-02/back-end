using Microsoft.Extensions.Localization;

namespace learning_center_webapi.Contexts.Tutorials.Domain.Exceptions;

public class DuplicateTutorialTitleException : Exception
{
    public string Title { get; }
    
    public DuplicateTutorialTitleException(string title)
        : base($"A tutorial with the title '{title}' already exists.")
    {
        Title = title;
    }
}

public class TutorialNotFoundException : Exception
{
    public Guid Id { get; }
    
    public TutorialNotFoundException(Guid id)
        : base($"A tutorial with the id '{id}' doesn't exists.")
    {
        Id = id;
    }
}