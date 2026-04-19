using DevFreela.Application.Common;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Features.Projects.CreateProject
{
    public class CreateProjectCommandHandler(IProjectRepository repository) : IRequestHandler<CreateProjectCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = request.ToEntity();
            await repository.AddAsync(project, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);

            return Result.Success(project.Id);
        }
    }
}
