using FluentValidation;

namespace DevFreela.Application.Features.Projects.CommentProject
{
    public class CommentProjectValidator : AbstractValidator<CommentProjectCommand>
    {
        public CommentProjectValidator()
        {
            RuleFor(c => c.Content)
                .NotEmpty();

            RuleFor(c => c.UserId)
                .NotNull();

            RuleFor(c => c.ProjectId)
                .NotNull();
        }
    }
}
