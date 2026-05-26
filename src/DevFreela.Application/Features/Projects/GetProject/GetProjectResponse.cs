using DevFreela.Core.Entities;
using DevFreela.Core.Enums;

namespace DevFreela.Application.Features.Projects.GetProject
{
    public class GetProjectResponse(Guid id, string title, string description, string clientName, string freelancerName, decimal totalCost, ProjectStatus status, List<ProjectComment> comments)
    {
        public Guid Id { get; } = id;
        public string Title { get; } = title;
        public string Description { get; } = description;
        public string ClientName { get; } = clientName;
        public string FreelancerName { get; } = freelancerName;
        public decimal TotalCost { get; } = totalCost;
        public ProjectStatus Status { get; } = status;
        public List<string> Comments { get; } = comments?.ConvertAll(c => c.Content) ?? [];

        public static GetProjectResponse FromEntity(Project project)
        {
            return new(
                project.Id,
                project.Title,
                project.Description,
                project.Client?.FullName ?? string.Empty,
                project.Freelancer?.FullName ?? string.Empty,
                project.TotalCost,
                project.Status,
                project.Comments);
        }
    }
}
