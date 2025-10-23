using learning_center_webapi.Contexts.Shared.Domain.Repositories;
using learning_center_webapi.Contexts.Shared.Infraestructure.Persistence.Configuration;

namespace learning_center_webapi.Contexts.Shared.Infraestructure.Repositories;

public class UnitOfWork(LearningCenterContext context) : IUnitOfWork
{
    public async Task CompleteAsync()
    {
        await context.SaveChangesAsync();
    }
}