using DevFreela.Application.Common;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Features.Users.AddSkills
{
    public class AddSkillsCommandHandler(DevFreelaDbContext context) : IRequestHandler
        <AddSkillsCommand, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(AddSkillsCommand request, CancellationToken cancellationToken)
        {
            var userSkills = request.SkillIds.ConvertAll(s => new UserSkill(request.UserId, s));
            
            await context.AddRangeAsync(userSkills, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return Result.Success(Unit.Value);
        }
    }
}
