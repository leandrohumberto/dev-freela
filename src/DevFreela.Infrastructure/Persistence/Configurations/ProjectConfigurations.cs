using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Infrastructure.Persistence.Configurations
{
    public class ProjectConfigurations : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(project => project.Id);

            builder.HasOne(project => project.Freelancer)
                .WithMany(freelancer => freelancer.FreelanceProjects)
                .HasForeignKey(project => project.FreelancerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(project => project.Client)
                .WithMany(client => client.OwnedProjects)
                .HasForeignKey(project => project.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.TotalCost)
                .HasColumnType("decimal(18,4)");
        }
    }
}
