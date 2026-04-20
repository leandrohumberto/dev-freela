using DevFreela.Application.Common;
using MediatR;

namespace DevFreela.Application.Features.Projects.DeleteProject
{
    public record DeleteProjectCommand(Guid ProjectId) : IRequest<Result>;
}
