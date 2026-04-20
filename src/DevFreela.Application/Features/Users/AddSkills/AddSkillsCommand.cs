using DevFreela.Application.Common;
using MediatR;
using System.Text.Json.Serialization;

namespace DevFreela.Application.Features.Users.AddSkills
{
    public record AddSkillsCommand(List<Guid> SkillIds) : IRequest<Result>
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
    }
}
