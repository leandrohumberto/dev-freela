using DevFreela.Application.Common;
using DevFreela.Core.Common;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Features.Users.AddSkills
{
    public class ValidateAddSkillsCommandBehavior(
        ISkillRepository repository)
        : IPipelineBehavior<AddSkillsCommand, Result>
    {
        public async Task<Result> Handle(AddSkillsCommand request, RequestHandlerDelegate<Result> next, CancellationToken cancellationToken)
        {
            foreach (var skillId in request.SkillIds)
            {
                if (!await repository.ExistsAsync(skillId, false, cancellationToken))
                {
                    return Result.Failure(ValidationRules.SkillNotFoundValidationMessage);
                }
            }

            return await next(cancellationToken);
        }
    }
}
