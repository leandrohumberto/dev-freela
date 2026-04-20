using DevFreela.Application.Common;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Features.Projects.GetProject
{
    public class GetProjectQueryHandler(IProjectRepository repository) : IRequestHandler<GetProjectQuery, Result<GetProjectResponse>>
    {
        public async Task<Result<GetProjectResponse>> Handle(GetProjectQuery request, CancellationToken cancellationToken)
        {
            var project = await repository.GetByIdAsync(request.Id, false, cancellationToken);

            if (project is null)
            {
                return Result.Failure<GetProjectResponse>("Project not found.");
            }
            var response = GetProjectResponse.FromEntity(project);
            return Result.Success(response);
        }
    }
}
