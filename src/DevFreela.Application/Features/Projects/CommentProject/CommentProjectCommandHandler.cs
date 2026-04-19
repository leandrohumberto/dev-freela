using DevFreela.Application.Common;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Features.Projects.CommentProject
{
    public class CommentProjectCommandHandler(DevFreelaDbContext context) : IRequestHandler<CommentProjectCommand, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(CommentProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await context.Projects.SingleOrDefaultAsync(p => p.Id == request.ProjectId && !p.Deleted, cancellationToken);

            if (project is null)
            {
                return Result.Failure<Unit>("Project not found.");
            }

            var comment = request.ToEntity();

            await context.ProjectComments.AddAsync(comment, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return Result.Success(Unit.Value);
        }
    }
}
