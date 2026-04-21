using MediatR;

namespace DevFreela.Application.Features.Projects.CreateProject.Notifications
{
    public record ProjectCreatedNotification(Guid ProjectId, string Title, decimal TotalCost) : INotification;
}
