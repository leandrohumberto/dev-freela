using DevFreela.Application.Common;
using MediatR;

namespace DevFreela.Application.Features.Skills.GetAllSkills
{
    public record GetAllSkillsQuery : IRequest<Result<List<GetAllSkillsResponse>>>;
}
