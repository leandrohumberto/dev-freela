using FluentValidation;

namespace DevFreela.Application.Features.Projects.CompleteProject
{
    public class CompleteProjectValidator : AbstractValidator<CompleteProjectCommand>
    {
        public CompleteProjectValidator()
        {
            RuleFor(x => x.ProjectId)
                .NotNull();
        }
    }
}
