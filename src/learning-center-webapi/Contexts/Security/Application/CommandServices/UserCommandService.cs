using learning_center_webapi.Contexts.Security.Domain.Commands;
using learning_center_webapi.Contexts.Security.Domain.Model.Entities;
using learning_center_webapi.Contexts.Security.Domain.Infraestructure;
using learning_center_webapi.Contexts.Security.Domain.Model.Exceptions;
using learning_center_webapi.Contexts.Shared.Domain.Repositories;

namespace learning_center_webapi.Contexts.Security.Application.CommandServices;

public class UserCommandService(IUserRepository userRepository,IHashService hashService,  IUnitOfWork unitOfWork) : IUserCommandService
{
    public async Task<User> Handle(CreateUserCommand command)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = command.Username,
            Password = hashService.HashPassword(command.Password) // hash password in a real scenario 
        };
        await userRepository.AddAsync(user);
        await unitOfWork.CompleteAsync();
        return user;
    }

    public async Task<bool> Handle(LoginUserCommand command)
    {
        var existingUser = await userRepository.FindByUsernameAsync(command.Username);
        
        if (existingUser == null)
        {
            throw new SecurityExceptions.LoginException();
        }
        
        
        var isPasswordValid = hashService.VerifyPassword(command.Password, existingUser.Password);

        if (!isPasswordValid)
        {
            throw new SecurityExceptions.LoginException();
        }

        return isPasswordValid;
    }
}