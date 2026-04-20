using DevFreela.Application.Common;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Features.Projects.StartProject
{
    public class StartProjectCommandHandler(IProjectRepository repository) : IRequestHandler<StartProjectCommand, Result>
    {
        public async Task<Result> Handle(StartProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await repository.GetByIdAsync(request.ProjectId, false, cancellationToken);

            if (project is null)
            {
                return Result.Failure("Project not found.");
            }

            project.Start();
            repository.Update(project);
            await repository.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
