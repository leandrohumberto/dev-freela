using DevFreela.Application.Common;
using MediatR;

namespace DevFreela.Application.Features.Projects.StartProject
{
    public record StartProjectCommand(Guid ProjectId) : IRequest<Result<Unit>>;
}
