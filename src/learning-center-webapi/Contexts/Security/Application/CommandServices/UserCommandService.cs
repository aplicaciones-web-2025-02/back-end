using learning_center_webapi.Contexts.Security.Domain.Commands;
using learning_center_webapi.Contexts.Security.Domain.Model.Entities;
using learning_center_webapi.Contexts.Security.Domain.Infraestructure;
using learning_center_webapi.Contexts.Security.Domain.Model.Exceptions;
using learning_center_webapi.Contexts.Shared.Domain.Repositories;

namespace learning_center_webapi.Contexts.Security.Application.CommandServices;

public class UserCommandService(
    IUserRepository userRepository,
    IHashService hashService,
    IUnitOfWork unitOfWork,
    IJWTEncrypter jwtEncrypter
) : IUserCommandService
{
    public async Task<User> Handle(CreateUserCommand command)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = command.Username,
            Password = hashService.HashPassword(command.Password),
            Profile = command.Profile
        };
        await userRepository.AddAsync(user);
        await unitOfWork.CompleteAsync();
        return user;
    }

    public async Task<string> Handle(LoginUserCommand command)
    {
        var user = await userRepository.FindByUsernameAsync(command.Username);
        if (user == null)
            throw new SecurityExceptions.LoginException();

        var isPasswordValid = hashService.VerifyPassword(command.Password, user.Password);
        if (!isPasswordValid)
            throw new SecurityExceptions.LoginException();

        return await jwtEncrypter.GenerateTokenAsync(user.Id, user.Username, user.Profile);
    }
}