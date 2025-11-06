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

    public async Task<Tutorial> Handle(UpdateTutorialCommand command)
    {
        var tutorial = await tutorialRepository.FindByIdAsync(command.Id);
        if (tutorial == null)
            throw new TutorialNotFoundException(command.Id);

        tutorial.Title = command.Title;
        tutorial.Description = command.Description;
        tutorial.PublishedDate = command.PublishedDate;
        tutorial.Author = command.Author;
        tutorial.AuthorEmail = command.AuthorEmail;
        tutorial.Level = command.Level;
        tutorial.Views = command.Views;
        tutorial.Tags = command.Tags;

        tutorialRepository.Update(tutorial);
        await unitOfWork.CompleteAsync();

        return tutorial;
    }

    public async Task<Tutorial> Handle(UpdateAuthorTutorialCommand command)
    {
        var tutorial = await tutorialRepository.FindByIdAsync(command.Id);
        if (tutorial == null)
            throw new TutorialNotFoundException(command.Id);

        tutorial.Author = command.Author;

        tutorialRepository.Update(tutorial);
        await unitOfWork.CompleteAsync();

        return tutorial;
    }

    public async Task<bool> Handle(DeleteTutorialCommand command)
    {
        var tutorial = await tutorialRepository.FindByIdAsync(command.Id);
        if (tutorial == null || tutorial.IsDeleted == 1)    
            throw new TutorialNotFoundException(command.Id);

        /*tutorialRepository.Remove(tutorial);*/

        tutorial.IsDeleted = 1;
        tutorialRepository.Update(tutorial);


        await unitOfWork.CompleteAsync();

        return true;
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