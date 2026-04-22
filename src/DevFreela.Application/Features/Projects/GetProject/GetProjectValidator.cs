using FluentValidation;

namespace DevFreela.Application.Features.Projects.GetProject
{
    public class GetProjectValidator : AbstractValidator<GetProjectQuery>
    {
        public GetProjectValidator()
        {
            RuleFor(x => x.ProjectId)
                .NotNull();
        }
    }
}
