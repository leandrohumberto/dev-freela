using DevFreela.Core.Enums;

namespace DevFreela.Core.Entities
{
    public class Project(string title, string description, Guid clientId, Guid freelancerId, decimal totalCost) : BaseEntity
    {
        public string Title { get; private set; } = title;
        public string Description { get; private set; } = description;
        public Guid ClientId { get; private set; } = clientId;
        public User? Client { get; init; }
        public Guid FreelancerId { get; private set; } = freelancerId;
        public User? Freelancer { get; init; }
        public decimal TotalCost { get; private set; } = totalCost;
        public DateTime? StartedAt { get; private set; }
        public DateTime? CompletedAt { get; private set; }
        public ProjectStatus Status { get; private set; } = ProjectStatus.Created;
        public List<ProjectComment> Comments { get; private set; } = [];

        public void Cancel()
        {
            if (Status == ProjectStatus.InProgress || Status == ProjectStatus.Suspended)
            {
                Status = ProjectStatus.Cancelled;
            }
        }

        public void Start()
        {
            if (Status == ProjectStatus.Created)
            {
                Status = ProjectStatus.InProgress;
                StartedAt = DateTime.Now;
            }
        }

        public void Complete()
        {
            if (Status == ProjectStatus.PendingPayment || Status == ProjectStatus.InProgress)
            {
                Status = ProjectStatus.Completed;
                CompletedAt = DateTime.Now;
            }
        }

        public void SetPaymentPending()
        {
            if (Status == ProjectStatus.InProgress)
            {
                Status = ProjectStatus.PendingPayment;
            }
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
