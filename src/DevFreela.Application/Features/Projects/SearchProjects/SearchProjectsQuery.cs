using DevFreela.Application.Common;
using MediatR;

namespace DevFreela.Application.Features.Projects.SearchProjects
{
    public record SearchProjectsQuery(string? Title) : IRequest<Result<List<SearchProjectsResponse>>>;
}
