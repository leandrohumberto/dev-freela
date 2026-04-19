using DevFreela.Application.Common;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Features.Projects.CommentProject
{
    public class CommentProjectCommandHandler(IProjectRepository repository) : IRequestHandler<CommentProjectCommand, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(CommentProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await repository.GetByIdAsync(request.ProjectId, false, cancellationToken);

            if (project is null)
            {
                return Result.Failure<Unit>("Project not found.");
            }

            var comment = request.ToEntity();

            await repository.AddCommentAsync(comment, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);

            return Result.Success(Unit.Value);
        }
    }
}
