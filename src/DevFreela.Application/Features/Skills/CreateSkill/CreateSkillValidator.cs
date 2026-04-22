using FluentValidation;

namespace DevFreela.Application.Features.Skills.CreateSkill
{
    public class CreateSkillValidator : AbstractValidator<CreateSkillCommand>
    {
        public CreateSkillValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty();
        }
    }
}
