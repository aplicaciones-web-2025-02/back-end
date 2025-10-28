using learning_center_webapi.Contexts.Tutorials.Domain.Model.Entities;
using learning_center_webapi.Contexts.Tutorials.Domain.Model.Queries;

namespace learning_center_webapi.Contexts.Tutorials.Domain.Queries;

public interface ITutorialQueryService
{
    Task<Tutorial?> Handle(GetByidTutorial query);
    Task<IEnumerable<Tutorial>> Handle(GetAllTutorials query);
}