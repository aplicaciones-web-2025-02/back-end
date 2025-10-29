using learning_center_webapi.Contexts.Enrolments.Domain.Infraestructure;
using learning_center_webapi.Contexts.Enrolments.Domain.Model.Aggregate;

namespace learning_center_webapi.Contexts.Enrolments.Application.QueryServices;

public class EnrolmentQueryService(IEnrolmentRepository enrolmentRepository) : IEnrolmentQueryService
{
    public async Task<IEnumerable<Enrolment>> GetAllAsync()
    {
        return await enrolmentRepository.ListAsync();
    }

    public async Task<Enrolment?> GetByIdAsync(int id)
    {
        return await enrolmentRepository.FindByIdAsync(id);
    }
}