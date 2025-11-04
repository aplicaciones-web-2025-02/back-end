using learning_center_webapi.Contexts.Security.Domain.Commands;
using learning_center_webapi.Contexts.Security.Domain.Model.Entities;
using learning_center_webapi.Contexts.Security.Domain.Infraestructure;
using learning_center_webapi.Contexts.Shared.Domain.Repositories;

namespace learning_center_webapi.Contexts.Security.Application.CommandServices;

public class UserCommandService(IUserRepository userRepository, IUnitOfWork unitOfWork) : IUserCommandService
{
    public async Task<User> Handle(CreateUserCommand command)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = command.Username,
            Password = command.Password 
        };
        await userRepository.AddAsync(user);
        await unitOfWork.CompleteAsync();
        return user;
    }
}