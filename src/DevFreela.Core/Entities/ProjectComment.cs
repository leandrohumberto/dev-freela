using DevFreela.Core.Common;

namespace DevFreela.Core.Entities
{
    public class ProjectComment : BaseEntity
    {
        public ProjectComment(string content, Guid projectId, Guid userId)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException(ValidationRules.RequiredProjectCommentContentDescriptionValidationMessage, nameof(content));

            Content = content;
            ProjectId = projectId;
            UserId = userId;
        }

        public string Content { get; private set; }
        public Guid ProjectId { get; private set; }
        public Project? Project { get; init; }
        public Guid UserId { get; private set; }
        public User? User { get; init; }
    }
}
