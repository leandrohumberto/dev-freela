using DevFreela.Application.Common;
using MediatR;
using System.Text.Json.Serialization;

namespace DevFreela.Application.Features.Projects.UpdateProject
{
    public record UpdateProjectCommand(string Title, string Description, decimal TotalCost) : IRequest<Result<Unit>>
    {
        [JsonIgnore]
        public Guid ProjectId { get; set; }
    }
}
