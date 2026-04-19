using DevFreela.Application.Common;
using DevFreela.Core.Entities;
using MediatR;
using System.Text.Json.Serialization;

namespace DevFreela.Application.Features.Projects.CommentProject
{
    public record CommentProjectCommand(Guid UserId, string Content) : IRequest<Result<Unit>>
    {
        [JsonIgnore]
        public Guid ProjectId { get; set; }

        public ProjectComment ToEntity() => new(Content, ProjectId, UserId);
    }
}
