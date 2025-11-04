using learning_center_webapi.Contexts.Shared.Infraestructure.Persistence.Configuration;
using learning_center_webapi.Contexts.Shared.Infraestructure.Repositories;
using learning_center_webapi.Contexts.Tutorials.Domain.Infraestructure;
using learning_center_webapi.Contexts.Tutorials.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace learning_center_webapi.Contexts.Tutorials.Infraestructure;

public class TutorialRepository(LearningCenterContext context) : BaseRepository<Tutorial>(context), ITutorialRepository
{
    private readonly LearningCenterContext _context = context;

    public async Task<IEnumerable<Tutorial>> GetTutorialsWithChaptersAsync()
    {
        var result = await _context.Set<Tutorial>().Include(t => t.Chapters).Where(t => t.IsDeleted ==0 ).ToListAsync();
        return result;
    }

    public async Task<Tutorial?> GetTutorialWithChaptersAsync(int id)
    {
        var result = await _context.Set<Tutorial>().Include(t => t.Chapters).FirstOrDefaultAsync(t => t.Id == id && t.IsDeleted==0);
        return result;
    }

    public async Task<Tutorial?> GetTutoriaByTitleAsync(string title)
    {
        var result = await _context.Set<Tutorial>().FirstOrDefaultAsync(t => t.Title == title && t.IsDeleted==0);
        return result;
    }
}