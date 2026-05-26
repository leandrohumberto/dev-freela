using DevFreela.Core.Common;

namespace DevFreela.Core.Entities
{
    public class Skill : BaseEntity
    {
        public Skill(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException(ValidationRules.RequiredSkillDescriptionValidationMessage, nameof(description));

            Description = description;
            UserSkills = [];
        }

        public string Description { get; private set; }
        public List<UserSkill> UserSkills { get; private set; }
    }
}
