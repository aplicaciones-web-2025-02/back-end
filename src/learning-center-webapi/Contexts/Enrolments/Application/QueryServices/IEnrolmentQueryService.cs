using learning_center_webapi.Contexts.Enrolments.Domain.Model.Aggregate;

namespace learning_center_webapi.Contexts.Enrolments.Application.QueryServices;

public interface IEnrolmentQueryService
{
    Task<IEnumerable<Enrolment>> GetAllAsync();
    Task<Enrolment?> GetByIdAsync(int id);
}