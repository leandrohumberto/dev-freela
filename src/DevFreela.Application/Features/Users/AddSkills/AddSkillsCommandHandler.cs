using DevFreela.Application.Common;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Features.Users.AddSkills
{
    public class AddSkillsCommandHandler(IUserRepository repository) : IRequestHandler
        <AddSkillsCommand, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(AddSkillsCommand request, CancellationToken cancellationToken)
        {
            var userSkills = request.SkillIds.ConvertAll(s => new UserSkill(request.UserId, s));
            
            await repository.AddSkillsAsync(userSkills, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);

            return Result.Success(Unit.Value);
        }
    }
}
