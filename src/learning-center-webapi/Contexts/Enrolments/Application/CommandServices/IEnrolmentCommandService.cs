using learning_center_webapi.Contexts.Enrolments.Domain.Model;
using learning_center_webapi.Contexts.Enrolments.Domain.Model.Aggregate;

namespace learning_center_webapi.Contexts.Enrolments.Application.CommandServices;

public interface IEnrolmentCommandService
{
    Task<Enrolment> CreateAsync(CreateEnrolmentCommand command);
}