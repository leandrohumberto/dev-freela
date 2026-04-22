using DevFreela.Application.Common;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Features.Projects.CompleteProject
{
    public class CompleteProjectCommandHandler(IProjectRepository repository) : IRequestHandler<CompleteProjectCommand, Result>
    {
        public async Task<Result> Handle(CompleteProjectCommand request, CancellationToken cancellationToken)
        {
            var exists = await repository.ExistsAsync(request.ProjectId, cancellationToken);

            if (!exists)
            {
                return Result.Failure("Project not found.");
            }

            var project = await repository.GetByIdAsync(request.ProjectId, false, cancellationToken);

            project!.Complete();
            repository.Update(project);
            await repository.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
