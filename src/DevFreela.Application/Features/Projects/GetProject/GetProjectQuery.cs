using DevFreela.Application.Common;
using MediatR;

namespace DevFreela.Application.Features.Projects.GetProject
{
    public record GetProjectQuery(Guid Id) : IRequest<Result<GetProjectResponse>>;
}
