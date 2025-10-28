using learning_center_webapi.Contexts.Shared.Domain.Repositories;
using learning_center_webapi.Contexts.Tutorials.Domain.Commands;
using learning_center_webapi.Contexts.Tutorials.Domain.Infraestructure;
using learning_center_webapi.Contexts.Tutorials.Domain.Model.Entities;

namespace learning_center_webapi.Contexts.Tutorials.Application.CommandServices;

public class TutorialCommandService : ITutorialCommandService
{
    private readonly ITutorialRepository _tutorialRepository;
    private readonly IUnitOfWork _unitOfWork;

    public TutorialCommandService(ITutorialRepository repository, IUnitOfWork unitOfWork)
    {
        _tutorialRepository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Tutorial> Handle(CreateTutorialCommand command)
    {
        var tutorial = new Tutorial
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


        //var existingTutorials = await _tutorialRepository.ListAsync();//error GRAVE
        //var duplicated = existingTutorials.FirstOrDefault(t => t.Title == tutorial.Title);

        var existingTutorial = await _tutorialRepository.GetTutoriaByTitleAsync(tutorial.Title);
        if (existingTutorial != null) throw new ArgumentException("Tutorial with the title already exists");


        await _tutorialRepository.AddAsync(tutorial);

        await _unitOfWork.CompleteAsync();

        return tutorial;
    }
}