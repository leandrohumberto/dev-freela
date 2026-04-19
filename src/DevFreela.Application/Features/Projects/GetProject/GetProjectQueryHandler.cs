using DevFreela.Application.Common;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Features.Projects.GetProject
{
    public class GetProjectQueryHandler(DevFreelaDbContext context) : IRequestHandler<GetProjectQuery, Result<GetProjectResponse>>
    {
        public async Task<Result<GetProjectResponse>> Handle(GetProjectQuery request, CancellationToken cancellationToken)
        {
            var project = await context.Projects
                 .Include(p => p.Client)
                 .Include(p => p.Freelancer)
                 .Include(p => p.Comments)
                 .Where(p => p.Id == request.Id && !p.Deleted)
                 .SingleOrDefaultAsync(cancellationToken);

            if (project is null)
            {
                return Result.Failure<GetProjectResponse>("Project not found.");
            }
            var response = GetProjectResponse.FromEntity(project);
            return Result.Success(response);
        }
    }
}
