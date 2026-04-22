using FluentValidation;

namespace DevFreela.Application.Features.Projects.SearchProjects
{
    public class SearchProjectsValidator : AbstractValidator<SearchProjectsQuery>
    {
        public SearchProjectsValidator()
        {
            RuleFor(x => x.Size)
                .GreaterThan(0);

            RuleFor(x => x.Page)
                .GreaterThanOrEqualTo(0);
        }
    }
}
