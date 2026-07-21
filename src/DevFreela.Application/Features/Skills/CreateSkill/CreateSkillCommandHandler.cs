using DevFreela.Application.Common;
using DevFreela.Core.Common;
using DevFreela.Core.Interfaces;
using MediatR;

namespace DevFreela.Application.Features.Skills.CreateSkill
{
    public class CreateSkillCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateSkillCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
        {
            if (await unitOfWork.Skills.ExistsAsync(request.Description, false, cancellationToken))
            {
                return Result.Failure<Guid>(ValidationRules.SkillAlreadyExistsValidationMessage);
            }

            var skill = request.ToEntity();
            await unitOfWork.Skills.AddAsync(skill, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);

            return Result.Success(skill.Id);
        }
    }
}
