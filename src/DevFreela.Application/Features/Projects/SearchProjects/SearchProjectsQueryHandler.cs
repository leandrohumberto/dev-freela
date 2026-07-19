using DevFreela.Application.Common;
using DevFreela.Core.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Features.Projects.SearchProjects
{
    public class SearchProjectsQueryHandler(IProjectRepository repository) : IRequestHandler<SearchProjectsQuery, Result<PaginationResult<SearchProjectsResponse>>>
    {
        public async Task<Result<PaginationResult<SearchProjectsResponse>>> Handle(SearchProjectsQuery request, CancellationToken cancellationToken)
        {
            var projectsPaginationResult = await repository.SearchAsync(
                request.Title,
                request.Description,
                request.Page,
                request.PageSize,
                false,
                cancellationToken);

            var responsePaginationResult = new PaginationResult<SearchProjectsResponse>(
                projectsPaginationResult.Page,
                projectsPaginationResult.TotalPages,
                projectsPaginationResult.PageSize,
                projectsPaginationResult.ItemsCount,
                projectsPaginationResult.Data.ConvertAll(SearchProjectsResponse.FromEntity));

            return Result.Success(responsePaginationResult);
        }
    }
}
