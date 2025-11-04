using learning_center_webapi.Contexts.Security.Domain.Infraestructure;
using learning_center_webapi.Contexts.Security.Domain.Model.Entities;

namespace learning_center_webapi.Contexts.Security.Application.QueryServices;

public class UserQueryService(IUserRepository userRepository) : IUserQueryService
{
    public async Task<IEnumerable<User>> ListAsync()
        => await userRepository.ListAsync();

    public async Task<User?> FindByIdAsync(Guid id)
        => await userRepository.FindByIdAsync(id);
}
