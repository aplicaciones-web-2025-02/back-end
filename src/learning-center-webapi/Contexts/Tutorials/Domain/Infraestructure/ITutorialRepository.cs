using learning_center_webapi.Contexts.Shared.Domain.Repositories;
using learning_center_webapi.Contexts.Tutorials.Domain.Model.Entities;

namespace learning_center_webapi.Contexts.Tutorials.Domain.Infraestructure;

public interface ITutorialRepository : IBaseRepository<Tutorial>
{
    Task<IEnumerable<Tutorial>> GetTutorialsWithChaptersAsync();
    Task<Tutorial> GetTutorialWithChaptersAsync(int id);

    Task<Tutorial?> GetTutoriaByTitleAsync(string title);
}