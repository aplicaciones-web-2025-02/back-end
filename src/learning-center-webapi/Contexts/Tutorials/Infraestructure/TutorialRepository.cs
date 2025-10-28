using learning_center_webapi.Contexts.Shared.Infraestructure.Persistence.Configuration;
using learning_center_webapi.Contexts.Shared.Infraestructure.Repositories;
using learning_center_webapi.Contexts.Tutorials.Domain.Infraestructure;
using learning_center_webapi.Contexts.Tutorials.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace learning_center_webapi.Contexts.Tutorials.Infraestructure;

public class TutorialRepository(LearningCenterContext context) : BaseRepository<Tutorial>(context), ITutorialRepository
{
    public async Task<IEnumerable<Tutorial>> GetTutorialsWithChaptersAsync()
    {
        var result = await context.Set<Tutorial>().Include(t => t.Chapters).ToListAsync();
        return result;
    }

    public async Task<Tutorial> GetTutorialWithChaptersAsync(int id)
    {
        var result = await context.Set<Tutorial>().Include(t => t.Chapters).FirstOrDefaultAsync(t => t.Id == id);
        return result;
    }

    public async Task<Tutorial?> GetTutoriaByTitleAsync(string title)
    {
        var result = await context.Set<Tutorial>().FirstOrDefaultAsync(t => t.Title == title);
        return result;
    }
}