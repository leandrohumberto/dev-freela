using DevFreela.Application.Common;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Features.Projects.CompleteProject
{
    public class CompleteProjectCommandHandler(IProjectRepository repository) : IRequestHandler<CompleteProjectCommand, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(CompleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await repository.GetByIdAsync(request.ProjectId, false, cancellationToken);

            if (project is null)
            {
                return Result.Failure<Unit>("Project not found.");
            }

            project.Complete();
            repository.Update(project);
            await repository.SaveChangesAsync(cancellationToken);

            return Result.Success(Unit.Value);
        }
    }
}
