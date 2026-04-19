using DevFreela.Application.Common;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Features.Projects.SearchProjects
{
    public class SearchProjectsQueryHandler(DevFreelaDbContext context) : IRequestHandler<SearchProjectsQuery, Result<List<SearchProjectsResponse>>>
    {
        public async Task<Result<List<SearchProjectsResponse>>> Handle(SearchProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = await context.Projects
                .Include(p => p.Client)
                .Include(p => p.Freelancer)
                .Where(p => p.Title == request.Title && !p.Deleted).ToListAsync(cancellationToken);

            var response = projects.ConvertAll(p => SearchProjectsResponse.FromEntity(p));

            return Result.Success(response);
        }
    }
}
