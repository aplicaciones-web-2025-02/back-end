using learning_center_webapi.Contexts.Enrolments.Domain.Model.Aggregate;
using learning_center_webapi.Contexts.Enrolments.Interfaces.REST.Resources;

namespace learning_center_webapi.Contexts.Enrolments.Interfaces.REST.Transform;

public static class EnrolmentResourceFromEntityAssembler
{
    public static EnrolmentResource ToResource(Enrolment entity)
    {
        return new EnrolmentResource
        {
            Id = entity.Id,
            UserId = entity.UserId,
            TutorialId = entity.TutorialId,
            EnrolmentDate = entity.EnrolmentDate,
            Progress = entity.Progress,
            CompletionDate = entity.CompletionDate
        };
    }
}