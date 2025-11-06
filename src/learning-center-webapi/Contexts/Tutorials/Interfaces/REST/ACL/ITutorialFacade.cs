namespace learning_center_webapi.Contexts.Tutorials.Interfaces.REST.ACL;

public interface ITutorialFacade
{
    Task<bool> IsValidTutorialId(Guid tutorialId);
}