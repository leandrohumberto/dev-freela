namespace DevFreela.Core.Entities
{
    public class User(string fullName, string email, DateOnly birthDate) : BaseEntity
    {
        public string FullName { get; private set; } = fullName;
        public string Email { get; private set; } = email;
        public DateOnly BirthDate { get; private set; } = birthDate;
        public bool Active { get; private set; } = true;
        public List<Project> OwnedProjects { get; private set; } = [];
        public List<Project> FreelanceProjects { get; private set; } = [];
        public List<ProjectComment> Comments { get; private set; } = [];
        public List<UserSkill> Skills { get; private set; } = [];
    }
}
