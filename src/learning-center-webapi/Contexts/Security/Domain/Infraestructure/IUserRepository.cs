using learning_center_webapi.Contexts.Security.Domain.Model.Entities;
using learning_center_webapi.Contexts.Shared.Domain.Repositories;

namespace learning_center_webapi.Contexts.Security.Domain.Infraestructure;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> FindByUsernameAsync(string username);
}