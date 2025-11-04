using learning_center_webapi.Contexts.Security.Domain.Commands;
using learning_center_webapi.Contexts.Security.Domain.Model.Entities;

namespace learning_center_webapi.Contexts.Security.Application.CommandServices;

public interface IUserCommandService
{
    Task<User> Handle(CreateUserCommand command);
}