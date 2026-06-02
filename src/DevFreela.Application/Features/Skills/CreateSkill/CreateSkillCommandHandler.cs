using DevFreela.Application.Common;
using DevFreela.Core.Common;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Features.Skills.CreateSkill
{
    public class CreateSkillCommandHandler(ISkillRepository repository) : IRequestHandler<CreateSkillCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
        {
            if (await repository.ExistsAsync(request.Description, false, cancellationToken))
            {
                return Result.Failure<Guid>(ValidationRules.SkillAlreadyExistsValidationMessage);
            }

            var skill = request.ToEntity();
            await repository.AddAsync(skill, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);

            return Result.Success(skill.Id);
        }
    }
}
