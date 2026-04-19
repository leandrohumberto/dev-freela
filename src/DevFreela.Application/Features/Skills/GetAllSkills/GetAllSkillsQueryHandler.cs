using DevFreela.Application.Common;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Features.Skills.GetAllSkills
{
    public class GetAllSkillsQueryHandler(DevFreelaDbContext context) : IRequestHandler<GetAllSkillsQuery, Result<List<GetAllSkillsResponse>>>
    {
        public async Task<Result<List<GetAllSkillsResponse>>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
        {
            var skills = await context.Skills.AsNoTracking().Select(s => new GetAllSkillsResponse(s.Id, s.Description)).ToListAsync(cancellationToken);

            return Result.Success(skills);
        }
    }
}
