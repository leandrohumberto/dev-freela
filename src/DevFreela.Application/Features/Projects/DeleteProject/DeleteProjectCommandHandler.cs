using DevFreela.Application.Common;
using DevFreela.Core.Common;
using DevFreela.Core.Interfaces;
using MediatR;

namespace DevFreela.Application.Features.Projects.DeleteProject
{
    public class DeleteProjectCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteProjectCommand, Result>
    {
        public async Task<Result> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var exists = await unitOfWork.Projects.ExistsAsync(request.ProjectId, cancellationToken);

            if (!exists)
            {
                return Result.Failure(ValidationRules.ProjectNotFoundValidationMessage);
            }

            var project = await unitOfWork.Projects.GetByIdAsync(request.ProjectId, false, cancellationToken);

            project!.Delete();
            unitOfWork.Projects.Update(project);
            await unitOfWork.CompleteAsync(cancellationToken);

            return Result.Success();
        }
    }
}
