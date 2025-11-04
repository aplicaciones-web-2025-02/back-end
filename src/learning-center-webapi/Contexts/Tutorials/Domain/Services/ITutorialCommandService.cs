using learning_center_webapi.Contexts.Tutorials.Domain.Model.Entities;

namespace learning_center_webapi.Contexts.Tutorials.Domain.Commands;

public interface ITutorialCommandService
{
    Task<Tutorial> Handle(CreateTutorialCommand command);
    Task<Tutorial> Handle(UpdateTutorialCommand command);
    
    Task<Tutorial> Handle(UpdateAuthorTutorialCommand command);

}