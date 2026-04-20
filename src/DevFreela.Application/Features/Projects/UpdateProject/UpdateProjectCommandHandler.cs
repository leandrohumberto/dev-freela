using DevFreela.Application.Common;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Features.Projects.UpdateProject
{
    public class UpdateProjectCommandHandler(IProjectRepository repository) : IRequestHandler<UpdateProjectCommand, Result>
    {
        public async Task<Result> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await repository.GetByIdAsync(request.ProjectId, false, cancellationToken);

            if (project is null)
            {
                return Result.Failure("Project not found.");
            }

            project.Update(request.Title, request.Description, request.TotalCost);
            repository.Update(project);
            await repository.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
