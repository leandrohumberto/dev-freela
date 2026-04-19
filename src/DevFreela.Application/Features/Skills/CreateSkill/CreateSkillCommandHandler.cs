using DevFreela.Application.Common;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Features.Skills.CreateSkill
{
    public class CreateSkillCommandHandler(DevFreelaDbContext context) : IRequestHandler<CreateSkillCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
        {
            var skill = request.ToEntity();
            await context.Skills.AddAsync(skill, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return Result.Success(skill.Id);
        }
    }
}
