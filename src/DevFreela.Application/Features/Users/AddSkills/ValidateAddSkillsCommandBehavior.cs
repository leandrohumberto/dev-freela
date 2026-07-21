using DevFreela.Application.Common;
using DevFreela.Core.Common;
using DevFreela.Core.Interfaces;
using MediatR;

namespace DevFreela.Application.Features.Users.AddSkills
{
    public class ValidateAddSkillsCommandBehavior(
        IUnitOfWork unitOfWork)
        : IPipelineBehavior<AddSkillsCommand, Result>
    {
        public async Task<Result> Handle(AddSkillsCommand request, RequestHandlerDelegate<Result> next, CancellationToken cancellationToken)
        {
            foreach (var skillId in request.SkillIds)
            {
                if (!await unitOfWork.Skills.ExistsAsync(skillId, false, cancellationToken))
                {
                    return Result.Failure(ValidationRules.SkillNotFoundValidationMessage);
                }
            }

            return await next(cancellationToken);
        }
    }
}
