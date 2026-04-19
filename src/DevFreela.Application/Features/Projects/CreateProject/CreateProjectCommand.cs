using DevFreela.Application.Common;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Features.Projects.CreateProject
{
    public record CreateProjectCommand(string Title, string Description, Guid ClientId, Guid FreelancerId, decimal TotalCost) : IRequest<Result<Guid>>
    {
        public Project ToEntity() => new(
            Title,
            Description,
            ClientId,
            FreelancerId,
            TotalCost);
    }
}
