namespace DevFreela.Core.Entities
{
    public class ProjectComment(string content, Guid projectId, Guid userId) : BaseEntity
    {
        public string Content { get; private set; } = content;
        public Guid ProjectId { get; private set; } = projectId;
        public Project? Project { get; init; }
        public Guid UserId { get; private set; } = userId;
        public User? User { get; init;  }
    }
}
