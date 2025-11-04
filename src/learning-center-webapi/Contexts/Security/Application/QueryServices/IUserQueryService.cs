using learning_center_webapi.Contexts.Security.Domain.Model.Entities;

namespace learning_center_webapi.Contexts.Security.Application.QueryServices;

public interface IUserQueryService
{
    Task<IEnumerable<User>> ListAsync();
    Task<User?> FindByIdAsync(Guid id);
}
