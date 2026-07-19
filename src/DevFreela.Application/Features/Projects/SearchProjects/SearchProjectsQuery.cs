using DevFreela.Application.Common;
using DevFreela.Core.Models;
using MediatR;

namespace DevFreela.Application.Features.Projects.SearchProjects
{
    public record SearchProjectsQuery(string? Title = "", string? Description = "", int Page = 1, int PageSize = 10) : IRequest<Result<PaginationResult<SearchProjectsResponse>>>;
}
