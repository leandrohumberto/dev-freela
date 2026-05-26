using DevFreela.Application.Common;
using DevFreela.Core.Common;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Features.Projects.GetProject
{
    public class GetProjectQueryHandler(IProjectRepository repository) : IRequestHandler<GetProjectQuery, Result<GetProjectResponse>>
    {
        public async Task<Result<GetProjectResponse>> Handle(GetProjectQuery request, CancellationToken cancellationToken)
        {
            var exists = await repository.ExistsAsync(request.ProjectId, cancellationToken);

            if (!exists)
            {
                return Result.Failure<GetProjectResponse>(ValidationRules.ProjectNotFoundValidationMessage);
            }

            var project = await repository.GetByIdAsync(request.ProjectId, false, cancellationToken);

            var response = GetProjectResponse.FromEntity(project!);
            return Result.Success(response);
        }
    }
}
