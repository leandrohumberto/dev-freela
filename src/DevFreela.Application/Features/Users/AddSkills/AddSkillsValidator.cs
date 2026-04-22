using FluentValidation;

namespace DevFreela.Application.Features.Users.AddSkills
{
    public class AddSkillsValidator : AbstractValidator<AddSkillsCommand>
    {
        public AddSkillsValidator()
        {
            RuleFor(x => x.UserId)
                .NotNull();

            RuleFor(x => x.SkillIds)
                .NotNull()
                .Must(ValidSkillIdsLength)
                    .WithMessage($"At least one skill is required.");
        }

        private static bool ValidSkillIdsLength(List<Guid> ids)
        {
            return ids.Count > 0;
        }
    }
}
