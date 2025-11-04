using learning_center_webapi.Contexts.Security.Domain.Model.Entities;
using learning_center_webapi.Contexts.Security.Domain.Infraestructure;
using Microsoft.EntityFrameworkCore;
using learning_center_webapi.Contexts.Shared.Infraestructure.Persistence.Configuration;
using learning_center_webapi.Contexts.Shared.Infraestructure.Repositories;

namespace learning_center_webapi.Contexts.Security.Infraestructure;

public class UserRepository(LearningCenterContext context) : BaseRepository<User>(context), IUserRepository
{

}