namespace DevFreela.Core.Entities
{
    public class Skill(string description) : BaseEntity
    {
        public string Description { get; private set; } = description;
        public List<UserSkill> UserSkills { get; private set; } = [];
    }
}
