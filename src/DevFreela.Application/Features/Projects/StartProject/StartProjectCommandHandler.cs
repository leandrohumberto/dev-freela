using DevFreela.Application.Common;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Features.Projects.StartProject
{
    public class StartProjectCommandHandler(DevFreelaDbContext context) : IRequestHandler<StartProjectCommand, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(StartProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await context.Projects.SingleOrDefaultAsync(p => p.Id == request.ProjectId && !p.Deleted, cancellationToken);

            if (project is null)
            {
                return Result.Failure<Unit>("Project not found.");
            }

            project.Start();
            context.Projects.Update(project);
            await context.SaveChangesAsync(cancellationToken);

            return Result.Success(Unit.Value);
        }
    }
}
