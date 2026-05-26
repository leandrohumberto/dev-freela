using DevFreela.Application.Common;
using DevFreela.Core.Common;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Features.Projects.StartProject
{
    public class StartProjectCommandHandler(IProjectRepository repository) : IRequestHandler<StartProjectCommand, Result>
    {
        public async Task<Result> Handle(StartProjectCommand request, CancellationToken cancellationToken)
        {
            var exists = await repository.ExistsAsync(request.ProjectId, cancellationToken);

            if (!exists)
            {
                return Result.Failure(ValidationRules.ProjectNotFoundValidationMessage);
            }

            var project = await repository.GetByIdAsync(request.ProjectId, false, cancellationToken);

            project!.Start();
            repository.Update(project);
            await repository.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
