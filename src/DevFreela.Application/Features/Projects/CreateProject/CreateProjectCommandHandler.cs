using DevFreela.Application.Common;
using DevFreela.Application.Features.Projects.CreateProject.Notifications;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Features.Projects.CreateProject
{
    public class CreateProjectCommandHandler(IProjectRepository repository, IMediator mediator) : IRequestHandler<CreateProjectCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = request.ToEntity();
            await repository.AddAsync(project, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);

            var projectCreated = new ProjectCreatedNotification(project.Id, project.Title, project.TotalCost);
            await mediator.Publish(projectCreated, cancellationToken);

            return Result.Success(project.Id);
        }
    }
}
