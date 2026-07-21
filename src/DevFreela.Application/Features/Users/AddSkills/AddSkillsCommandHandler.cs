using DevFreela.Application.Common;
using DevFreela.Core.Common;
using DevFreela.Core.Entities;
using DevFreela.Core.Interfaces;
using MediatR;

namespace DevFreela.Application.Features.Users.AddSkills
{
    public class AddSkillsCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler
        <AddSkillsCommand, Result>
    {
        public async Task<Result> Handle(AddSkillsCommand request, CancellationToken cancellationToken)
        {
            if (request.SkillIds == null)
            {
                return Result.Failure(ValidationRules.RequiredSkillIdsValidationMessage);
            }

            if (!await unitOfWork.Users.ExistsAsync(request.UserId, cancellationToken))
            {
                return Result.Failure(ValidationRules.UserNotFoundValidationMessage);
            }

            var userSkills = request.SkillIds.ConvertAll(s => new UserSkill(request.UserId, s));

            await unitOfWork.Users.AddSkillsAsync(userSkills, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);

            return Result.Success();
        }
    }
}
