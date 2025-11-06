using learning_center_webapi.Contexts.Enrolments.Domain.Infraestructure;
using learning_center_webapi.Contexts.Enrolments.Domain.Model;
using learning_center_webapi.Contexts.Enrolments.Domain.Model.Aggregate;
using learning_center_webapi.Contexts.Enrolments.Domain.Model.Exceptions;
using learning_center_webapi.Contexts.Security.Domain.Infraestructure;
using learning_center_webapi.Contexts.Shared.Domain.Repositories;
using learning_center_webapi.Contexts.Tutorials.Domain.Infraestructure;
using learning_center_webapi.Contexts.Tutorials.Interfaces.REST.ACL;

namespace learning_center_webapi.Contexts.Enrolments.Application.CommandServices;

public class EnrolmentCommandService(IEnrolmentRepository enrolmentRepository,
    ITutorialFacade tutorialFacade,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
    : IEnrolmentCommandService
{
    public async Task<Enrolment> CreateAsync(CreateEnrolmentCommand command)
    {
        
      /*  var tutorial = await tutorialRepository.FindByIdAsync(command.TutorialId);
        if (tutorial == null)
        {
            throw new KeyNotFoundException($"Tutorial with id {command.TutorialId} not found.");
        }*/
      
      
        var isValid =  await tutorialFacade.IsValidTutorialId(command.TutorialId);
        if (!isValid)
        {
            throw new TutorialNotExistExceptions(command.TutorialId);
        }
      
        
        var user = await userRepository.FindByIdAsync(command.UserId);
        if (user == null)
        {
            throw new UserNotExistExceptions(command.UserId);
        }
        
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