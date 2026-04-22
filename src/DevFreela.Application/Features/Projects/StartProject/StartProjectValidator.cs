using FluentValidation;

namespace DevFreela.Application.Features.Projects.StartProject
{
    internal class StartProjectValidator : AbstractValidator<StartProjectCommand>
    {
        public StartProjectValidator()
        {
            RuleFor(x => x.ProjectId)
                .NotNull();
        }
    }
}
