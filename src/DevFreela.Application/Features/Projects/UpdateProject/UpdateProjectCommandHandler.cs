using DevFreela.Application.Common;
using DevFreela.Core.Common;
using DevFreela.Core.Interfaces;
using MediatR;

namespace DevFreela.Application.Features.Projects.UpdateProject
{
    public class UpdateProjectCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateProjectCommand, Result>
    {
        public async Task<Result> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var exists = await unitOfWork.Projects.ExistsAsync(request.ProjectId, cancellationToken);

            if (!exists)
            {
                return Result.Failure(ValidationRules.ProjectNotFoundValidationMessage);
            }

            var project = await unitOfWork.Projects.GetByIdAsync(request.ProjectId, false, cancellationToken);

            project!.Update(request.Title, request.Description, request.TotalCost);
            unitOfWork.Projects.Update(project);
            await unitOfWork.CompleteAsync(cancellationToken);

            return Result.Success();
        }
    }
}
