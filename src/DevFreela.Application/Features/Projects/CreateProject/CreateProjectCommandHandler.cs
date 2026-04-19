using DevFreela.Application.Common;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Features.Projects.CreateProject
{
    public class CreateProjectCommandHandler(DevFreelaDbContext context) : IRequestHandler<CreateProjectCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = request.ToEntity();
            await context.Projects.AddAsync(project, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return Result.Success(project.Id);
        }
    }
}
