using DevFreela.Application.Common;
using DevFreela.Core.Common;
using DevFreela.Core.Interfaces;
using MediatR;

namespace DevFreela.Application.Features.Projects.StartProject
{
    public class StartProjectCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<StartProjectCommand, Result>
    {
        public async Task<Result> Handle(StartProjectCommand request, CancellationToken cancellationToken)
        {
            var exists = await unitOfWork.Projects.ExistsAsync(request.ProjectId, cancellationToken);

            if (!exists)
            {
                return Result.Failure(ValidationRules.ProjectNotFoundValidationMessage);
            }

            var project = await unitOfWork.Projects.GetByIdAsync(request.ProjectId, false, cancellationToken);

            project!.Start();
            unitOfWork.Projects.Update(project);
            await unitOfWork.CompleteAsync(cancellationToken);

            return Result.Success();
        }
    }
}
