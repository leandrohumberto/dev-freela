using DevFreela.Core.Common;
using FluentValidation;

namespace DevFreela.Application.Features.Projects.CreateProject
{
    public class CreateProjectValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectValidator()
        {
            RuleFor(p => p.Description)
                .NotNull()
                .NotEmpty()
                .MaximumLength(ValidationRules.ProjectDescriptionMaximumLength);

            RuleFor(p => p.Title)
                .NotNull()
                .NotEmpty()
                .MaximumLength(ValidationRules.ProjectTitleMaximumLength);

            RuleFor(p => p.TotalCost)
                .GreaterThan(0M);

            RuleFor(p => p.ClientId).NotNull();

            RuleFor(p => p.FreelancerId)
                .NotNull();
        }
    }
}
