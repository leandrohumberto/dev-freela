using DevFreela.Application.Common;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Features.Projects.UpdateProject
{
    public class UpdateProjectCommandHandler(DevFreelaDbContext context) : IRequestHandler<UpdateProjectCommand, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await context.Projects.SingleOrDefaultAsync(p => p.Id == request.ProjectId && !p.Deleted, cancellationToken);

            if (project is null)
            {
                return Result.Failure<Unit>("Project not found.");
            }

            project.Update(request.Title, request.Description, request.TotalCost);
            context.Projects.Update(project);
            await context.SaveChangesAsync(cancellationToken);

            return Result.Success(Unit.Value);
        }
    }
}
