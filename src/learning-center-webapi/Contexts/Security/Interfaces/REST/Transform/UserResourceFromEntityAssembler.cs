using learning_center_webapi.Contexts.Security.Domain.Model.Entities;
using learning_center_webapi.Contexts.Security.Interfaces.REST.Resources;

namespace learning_center_webapi.Contexts.Security.Interfaces.REST.Transform;

public static class UserResourceFromEntityAssembler
{
    public static UserResource ToResource(User user)
    {
        return new UserResource
        {
            Id = user.Id,
            Username = user.Username
        };
    }
}
