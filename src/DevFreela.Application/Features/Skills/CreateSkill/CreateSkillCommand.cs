using DevFreela.Application.Common;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Features.Skills.CreateSkill
{
    public record CreateSkillCommand(string Description) : IRequest<Result<Guid>>
    {
        public Skill ToEntity() => new(Description);
    }
}
