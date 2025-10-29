using learning_center_webapi.Contexts.Enrolments.Domain.Infraestructure;
using learning_center_webapi.Contexts.Enrolments.Domain.Model.Aggregate;
using learning_center_webapi.Contexts.Shared.Infraestructure.Persistence.Configuration;
using learning_center_webapi.Contexts.Shared.Infraestructure.Repositories;

namespace learning_center_webapi.Contexts.Enrolments.Infraestructure;

public class EnrolmentRepository(LearningCenterContext context)
    : BaseRepository<Enrolment>(context), IEnrolmentRepository
{
}