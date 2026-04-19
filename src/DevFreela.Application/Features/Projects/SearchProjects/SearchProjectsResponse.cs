using DevFreela.Core.Entities;

namespace DevFreela.Application.Features.Projects.SearchProjects
{
    public class SearchProjectsResponse(Guid id, string title, string clientName, string freelancerName, decimal totalCost)
    {
        public Guid Id { get; } = id;
        public string Title { get; } = title;
        public string ClientName { get; } = clientName;
        public string FreelancerName { get; } = freelancerName;
        public decimal TotalCost { get; } = totalCost;

        public static SearchProjectsResponse FromEntity(Project project)
        {
            return new(
                project.Id,
                project.Title,
                project!.Client!.FullName,
                project.Freelancer!.FullName,
                project.TotalCost);
        }
    }
}
