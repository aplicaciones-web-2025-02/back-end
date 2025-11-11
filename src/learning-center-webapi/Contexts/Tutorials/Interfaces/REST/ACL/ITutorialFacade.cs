namespace learning_center_webapi.Contexts.Tutorials.Interfaces.REST.ACL;

public interface ITutorialFacade
{
    /// <summary>
    ///  Validates if a tutorial ID exists.
    /// </summary>
    Task<bool> IsValidTutorialId(Guid tutorialId);
}