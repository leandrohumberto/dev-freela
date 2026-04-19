using DevFreela.Application.Common;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Features.Projects.UpdateProject
{
    public class UpdateProjectCommandHandler(IProjectRepository repository) : IRequestHandler<UpdateProjectCommand, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await repository.GetByIdAsync(request.ProjectId, false, cancellationToken);

            if (project is null)
            {
                return Result.Failure<Unit>("Project not found.");
            }

            project.Update(request.Title, request.Description, request.TotalCost);
            repository.Update(project);
            await repository.SaveChangesAsync(cancellationToken);

            return Result.Success(Unit.Value);
        }
    }
}
