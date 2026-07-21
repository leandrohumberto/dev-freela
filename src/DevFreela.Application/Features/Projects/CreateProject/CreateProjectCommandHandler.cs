using DevFreela.Application.Common;
using DevFreela.Application.Features.Projects.CreateProject.Notifications;
using DevFreela.Core.Interfaces;
using MediatR;

namespace DevFreela.Application.Features.Projects.CreateProject
{
    public class CreateProjectCommandHandler(IUnitOfWork unitOfWork, IMediator mediator) : IRequestHandler<CreateProjectCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = request.ToEntity();
            await unitOfWork.Projects.AddAsync(project, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);

            var projectCreated = new ProjectCreatedNotification(project.Id, project.Title, project.TotalCost);
            await mediator.Publish(projectCreated, cancellationToken);

            return Result.Success(project.Id);
        }
    }
}
