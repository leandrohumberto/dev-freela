using DevFreela.Application.Common;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Features.Projects.DeleteProject
{
    public class DeleteProjectCommandHandler(IProjectRepository repository) : IRequestHandler<DeleteProjectCommand, Result>
    {
        public async Task<Result> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var exists = await repository.ExistsAsync(request.ProjectId, cancellationToken);

            if (!exists)
            {
                return Result.Failure("Project not found.");
            }

            var project = await repository.GetByIdAsync(request.ProjectId, false, cancellationToken);

            project!.Delete();
            repository.Update(project);
            await repository.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
