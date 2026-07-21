using DevFreela.Application.Common;
using DevFreela.Core.Common;
using DevFreela.Core.Interfaces;
using MediatR;

namespace DevFreela.Application.Features.Projects.GetProject
{
    public class GetProjectQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetProjectQuery, Result<GetProjectResponse>>
    {
        public async Task<Result<GetProjectResponse>> Handle(GetProjectQuery request, CancellationToken cancellationToken)
        {
            var exists = await unitOfWork.Projects.ExistsAsync(request.ProjectId, cancellationToken);

            if (!exists)
            {
                return Result.Failure<GetProjectResponse>(ValidationRules.ProjectNotFoundValidationMessage);
            }

            var project = await unitOfWork.Projects.GetByIdAsync(request.ProjectId, false, cancellationToken);

            var response = GetProjectResponse.FromEntity(project!);
            return Result.Success(response);
        }
    }
}
