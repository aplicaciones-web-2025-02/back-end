using learning_center_webapi.Contexts.Enrolments.Domain.Model.Aggregate;
using learning_center_webapi.Contexts.Shared.Domain.Repositories;

namespace learning_center_webapi.Contexts.Enrolments.Domain.Infraestructure;

public interface IEnrolmentRepository : IBaseRepository<Enrolment>
{
}