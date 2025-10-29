using learning_center_webapi.Contexts.Tutorials.Domain.Infraestructure;
using learning_center_webapi.Contexts.Tutorials.Domain.Model.Entities;
using learning_center_webapi.Contexts.Tutorials.Domain.Model.Queries;
using learning_center_webapi.Contexts.Tutorials.Domain.Queries;

namespace learning_center_webapi.Contexts.Tutorials.Application.QueryServices;

public class TutorialQueryService(ITutorialRepository tutorialRepository) : ITutorialQueryService
{
    private readonly ITutorialRepository _tutorialRepository = tutorialRepository;

    // Classic constructor (commented for reference)
    /*
    public TutorialQueryService(ITutorialRepository tutorialRepository)
    {
        _tutorialRepository = tutorialRepository;
    }
    */

    public async Task<Tutorial?> Handle(GetByidTutorial query)
    {
        //return  await _tutorialRepository.FindByIdAsync(tutorialId);
        return await _tutorialRepository.GetTutorialWithChaptersAsync(query.Id);
    }

    public async Task<IEnumerable<Tutorial>> Handle(GetAllTutorials query)
    {
        return await _tutorialRepository.GetTutorialsWithChaptersAsync();
    }
}