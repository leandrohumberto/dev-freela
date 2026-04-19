using DevFreela.Application.Common;
using MediatR;

namespace DevFreela.Application.Features.Projects.CompleteProject
{
    public record CompleteProjectCommand(Guid ProjectId) : IRequest<Result<Unit>>;
}
