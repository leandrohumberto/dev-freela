using DevFreela.Application.Common;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Features.Projects.CommentProject
{
    public class CommentProjectCommandHandler(IProjectRepository repository) : IRequestHandler<CommentProjectCommand, Result>
    {
        public async Task<Result> Handle(CommentProjectCommand request, CancellationToken cancellationToken)
        {
            var exists = await repository.ExistsAsync(request.ProjectId, cancellationToken);

            if (!exists)
            {
                return Result.Failure("Project not found.");
            }

            var comment = request.ToEntity();

            await repository.AddCommentAsync(comment, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
