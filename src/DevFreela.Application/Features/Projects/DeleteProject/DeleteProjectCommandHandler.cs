using DevFreela.Application.Common;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Features.Projects.DeleteProject
{
    public class DeleteProjectCommandHandler(DevFreelaDbContext context) : IRequestHandler<DeleteProjectCommand, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await context.Projects.SingleOrDefaultAsync(p => p.Id == request.ProjectId && !p.Deleted, cancellationToken);

            if (project is null)
            {
                return Result.Failure<Unit>("Project not found.");
            }

            project.Delete();
            context.Projects.Update(project);
            await context.SaveChangesAsync(cancellationToken);

            return Result.Success(Unit.Value);
        }
    }
}
