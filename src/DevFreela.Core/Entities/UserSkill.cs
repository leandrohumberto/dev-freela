namespace DevFreela.Core.Entities
{
    public class UserSkill(Guid userId, Guid skillId) : BaseEntity
    {
        public Guid UserId { get; private set; } = userId;
        public User? User { get; init; }
        public Guid SkillId { get; private set; } = skillId;
        public Skill? Skill { get; init; }
    }
}
