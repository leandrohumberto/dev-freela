using DevFreela.Application.Common;
using DevFreela.Core.Interfaces;
using MediatR;

namespace DevFreela.Application.Features.Skills.GetAllSkills
{
    public class GetAllSkillsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllSkillsQuery, Result<List<GetAllSkillsResponse>>>
    {
        public async Task<Result<List<GetAllSkillsResponse>>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
        {
            var skills = await unitOfWork.Skills.GetAllAsync(false, cancellationToken);
            var response = skills.ConvertAll(s => new GetAllSkillsResponse(s.Id, s.Description));

            return Result.Success(response);
        }
    }
}
