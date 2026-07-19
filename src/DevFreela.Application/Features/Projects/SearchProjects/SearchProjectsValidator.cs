using FluentValidation;

namespace DevFreela.Application.Features.Projects.SearchProjects
{
    public class SearchProjectsValidator : AbstractValidator<SearchProjectsQuery>
    {
        public SearchProjectsValidator()
        {
            RuleFor(x => x.PageSize)
                .GreaterThan(0);

            RuleFor(x => x.Page)
                .GreaterThan(0);
        }
    }
}
