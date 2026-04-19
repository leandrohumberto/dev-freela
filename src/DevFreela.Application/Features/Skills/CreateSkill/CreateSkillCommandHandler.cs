using DevFreela.Application.Common;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Features.Skills.CreateSkill
{
    public class CreateSkillCommandHandler(ISkillRepository repository) : IRequestHandler<CreateSkillCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
        {
            var skill = request.ToEntity();
            await repository.AddAsync(skill, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);

            return Result.Success(skill.Id);
        }
    }
}
