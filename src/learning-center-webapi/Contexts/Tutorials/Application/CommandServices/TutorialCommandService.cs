using learning_center_webapi.Contexts.Shared.Domain.Repositories;
using learning_center_webapi.Contexts.Tutorials.Domain.Commands;
using learning_center_webapi.Contexts.Tutorials.Domain.Exceptions;
using learning_center_webapi.Contexts.Tutorials.Domain.Infraestructure;
using learning_center_webapi.Contexts.Tutorials.Domain.Model.Entities;

namespace learning_center_webapi.Contexts.Tutorials.Application.CommandServices;

public class TutorialCommandService(ITutorialRepository tutorialRepository, IUnitOfWork unitOfWork)
    : ITutorialCommandService
{
    private const int MaxTutorialsPerAuthor = 10;
    private const int MinLevel = 1;
    private const int MaxLevel = 5;

    //private readonly ITutorialRepository _tutorialRepository = tutorialRepository;
    //private readonly IUnitOfWork _unitOfWork = unitOfWork;

    // Classic constructor (commented for reference)
    /*
    public TutorialCommandService(ITutorialRepository repository, IUnitOfWork unitOfWork)
    {
        _tutorialRepository = repository;
        _unitOfWork = unitOfWork;
    }
    */

    public async Task<Tutorial> Handle(CreateTutorialCommand command)
    {
        var tutorial = CreateTutorialFromCommand(command);
        var authorTutorials = await tutorialRepository.GetTutorialsWithChaptersAsync();

        await ValidateDuplicateTitle(tutorial.Title);

        await tutorialRepository.AddAsync(tutorial);
        await unitOfWork.CompleteAsync();
        return tutorial;
    }

    private Tutorial CreateTutorialFromCommand(CreateTutorialCommand command)
    {
        return new Tutorial
        {
            Title = command.Title,
            Description = command.Description,
            PublishedDate = command.PublishedDate,
            Author = command.Author,
            AuthorEmail = command.AuthorEmail,
            Level = command.Level,
            Views = command.Views,
            Tags = command.Tags,
            CreatedDate = DateTime.Now
        };
    }

    private async Task ValidateDuplicateTitle(string title)
    {
        var existingTutorial = await tutorialRepository.GetTutoriaByTitleAsync(title);
        if (existingTutorial != null)
            throw new DuplicateTutorialTitleException(title);
    }
}