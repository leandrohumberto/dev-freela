using DevFreela.Application.Common;
using MediatR;

namespace DevFreela.Application.Features.Projects.SearchProjects
{
    public record SearchProjectsQuery(string? Title = "", string? Description = "", int Page = 0, int Size = 100) : IRequest<Result<List<SearchProjectsResponse>>>;
}
