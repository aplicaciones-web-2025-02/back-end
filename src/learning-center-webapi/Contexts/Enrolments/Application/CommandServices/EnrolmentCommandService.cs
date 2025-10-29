using learning_center_webapi.Contexts.Enrolments.Domain.Infraestructure;
using learning_center_webapi.Contexts.Enrolments.Domain.Model;
using learning_center_webapi.Contexts.Enrolments.Domain.Model.Aggregate;
using learning_center_webapi.Contexts.Shared.Domain.Repositories;

namespace learning_center_webapi.Contexts.Enrolments.Application.CommandServices;

public class EnrolmentCommandService(IEnrolmentRepository enrolmentRepository, IUnitOfWork unitOfWork)
    : IEnrolmentCommandService
{
    public async Task<Enrolment> CreateAsync(CreateEnrolmentCommand command)
    {
        var enrolment = new Enrolment
        {
            UserId = command.UserId,
            TutorialId = command.TutorialId,
            EnrolmentDate = command.EnrolmentDate,
            CreatedDate = DateTime.UtcNow
        };
        await enrolmentRepository.AddAsync(enrolment);
        await unitOfWork.CompleteAsync();
        return enrolment;
    }
}