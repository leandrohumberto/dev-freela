using DevFreela.Core.Common;
using DevFreela.Core.Enums;

namespace DevFreela.Core.Entities
{
    public class Project : BaseEntity
    {
        public Project(string title, string description, Guid clientId, Guid freelancerId, decimal totalCost)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException(ValidationRules.RequiredProjectTitleValidationMessage, nameof(title));

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException(ValidationRules.RequiredProjectDescriptionValidationMessage, nameof(description));

            if (totalCost < ValidationRules.ProjectTotalCostMinimumValue)
                throw new ArgumentException(ValidationRules.InvalidProjectTotalCostMinimumValueValidationMessage, nameof(totalCost));

            Title = title;
            Description = description;
            ClientId = clientId;
            FreelancerId = freelancerId;
            TotalCost = totalCost;
            Status = ProjectStatus.Created;
            Comments = [];
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public Guid ClientId { get; private set; }
        public User? Client { get; init; }
        public Guid FreelancerId { get; private set; }
        public User? Freelancer { get; init; }
        public decimal TotalCost { get; private set; }
        public DateTime? StartedAt { get; private set; }
        public DateTime? CompletedAt { get; private set; }
        public ProjectStatus Status { get; private set; }
        public List<ProjectComment> Comments { get; private set; }

        public void Cancel()
        {
            if (Status != ProjectStatus.InProgress && Status != ProjectStatus.Suspended)
            {
                throw new InvalidOperationException(ValidationRules.InvalidProjectStateValidationMessage);
            }

            Status = ProjectStatus.Cancelled;
        }

        public void Start()
        {
            if (Status != ProjectStatus.Created)
            {
                throw new InvalidOperationException(ValidationRules.InvalidProjectStateValidationMessage);
            }

            Status = ProjectStatus.InProgress;
            StartedAt = DateTime.Now;
        }

        public void Complete()
        {
            if (Status != ProjectStatus.PendingPayment && Status != ProjectStatus.InProgress)
            {
                throw new InvalidOperationException(ValidationRules.InvalidProjectStateValidationMessage);
            }

            Status = ProjectStatus.Completed;
            CompletedAt = DateTime.Now;
        }

        public void SetPaymentPending()
        {
            if (Status != ProjectStatus.InProgress)
            {
                throw new InvalidOperationException(ValidationRules.InvalidProjectStateValidationMessage);
            }

            Status = ProjectStatus.PendingPayment;
        }

        public void Update(string title, string description, decimal totalCost)
        {
            Title = title;
            Description = description;
            TotalCost = totalCost;
        }

        public override void Delete()
        {
            base.Delete();

            Comments.ForEach(c => c.Delete());
        }
    }
}
