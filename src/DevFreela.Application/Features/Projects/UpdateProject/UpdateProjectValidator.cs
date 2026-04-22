using DevFreela.Core.Common;
using FluentValidation;

namespace DevFreela.Application.Features.Projects.UpdateProject
{
    public class UpdateProjectValidator : AbstractValidator<UpdateProjectCommand>
    {
        public UpdateProjectValidator()
        {
            RuleFor(x => x.ProjectId)
                .NotNull();

            RuleFor(p => p.Description)
                .NotEmpty()
                .MaximumLength(ValidationRules.ProjectDescriptionMaximumLength);

            RuleFor(p => p.Title)
                .NotEmpty()
                .MaximumLength(ValidationRules.ProjectTitleMaximumLength);

            RuleFor(p => p.TotalCost)
                .GreaterThan(0M);
        }
    }
}
