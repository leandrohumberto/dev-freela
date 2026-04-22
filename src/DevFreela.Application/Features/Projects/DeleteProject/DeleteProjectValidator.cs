using FluentValidation;

namespace DevFreela.Application.Features.Projects.DeleteProject
{
    public class DeleteProjectValidator : AbstractValidator<DeleteProjectCommand>
    {
        public DeleteProjectValidator()
        {
            RuleFor(x => x.ProjectId)
                .NotNull();
        }
    }
}
