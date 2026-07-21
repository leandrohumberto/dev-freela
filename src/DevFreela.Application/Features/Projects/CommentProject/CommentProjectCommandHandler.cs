using DevFreela.Application.Common;
using DevFreela.Core.Common;
using DevFreela.Core.Interfaces;
using MediatR;

namespace DevFreela.Application.Features.Projects.CommentProject
{
    public class CommentProjectCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CommentProjectCommand, Result>
    {
        public async Task<Result> Handle(CommentProjectCommand request, CancellationToken cancellationToken)
        {
            var exists = await unitOfWork.Projects.ExistsAsync(request.ProjectId, cancellationToken);

            if (!exists)
            {
                return Result.Failure(ValidationRules.ProjectNotFoundValidationMessage);
            }

            var comment = request.ToEntity();

            await unitOfWork.Projects.AddCommentAsync(comment, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);

            return Result.Success();
        }
    }
}
