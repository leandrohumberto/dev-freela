using DevFreela.Application.Common;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Features.Projects.SearchProjects
{
    public class SearchProjectsQueryHandler(IProjectRepository repository) : IRequestHandler<SearchProjectsQuery, Result<List<SearchProjectsResponse>>>
    {
        public async Task<Result<List<SearchProjectsResponse>>> Handle(SearchProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = await repository.SearchAsync(
                request.Title,
                request.Description,
                request.Page,
                request.Size,
                false,
                cancellationToken);

            var response = projects.ConvertAll(p => SearchProjectsResponse.FromEntity(p));

            return Result.Success(response);
        }
    }
}
