using learning_center_webapi.Contexts.Tutorials.Domain.Model.Queries;
using learning_center_webapi.Contexts.Tutorials.Domain.Queries;
using learning_center_webapi.Contexts.Tutorials.Interfaces.REST.ACL;

namespace learning_center_webapi.Contexts.Tutorials.Application.ACL;

public class TutorialFacade(ITutorialQueryService tutorialQueryService) : ITutorialFacade
{
    public async Task<bool> IsValidTutorialId(Guid tutorialId)
    {
        var tutorialQuery = new GetByidTutorial(tutorialId);
        var existingTutorial = await tutorialQueryService.Handle(tutorialQuery);
        
        var isValid = (existingTutorial != null) ? true : false;
        return isValid;
  
    }
}