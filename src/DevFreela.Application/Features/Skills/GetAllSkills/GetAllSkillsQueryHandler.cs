using DevFreela.Application.Common;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Features.Skills.GetAllSkills
{
    public class GetAllSkillsQueryHandler(ISkillRepository repository) : IRequestHandler<GetAllSkillsQuery, Result<List<GetAllSkillsResponse>>>
    {
        public async Task<Result<List<GetAllSkillsResponse>>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
        {
            var skills = await repository.GetAllAsync(false, cancellationToken);
            var response = skills.ConvertAll(s => new GetAllSkillsResponse(s.Id, s.Description));

            return Result.Success(response);
        }
    }
}
