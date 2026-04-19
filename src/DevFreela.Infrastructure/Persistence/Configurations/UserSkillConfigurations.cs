using DevFreela.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFreela.Infrastructure.Persistence.Configurations
{
    public class UserSkillConfigurations : IEntityTypeConfiguration<UserSkill>
    {
        public void Configure(EntityTypeBuilder<UserSkill> builder)
        {
            builder.HasKey(userSkill => userSkill.Id);

            builder.HasOne(userSkill => userSkill.Skill)
                .WithMany(skill => skill.UserSkills)
                .HasForeignKey(userSkill => userSkill.SkillId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(userSkill => userSkill.User)
                .WithMany(user => user.Skills)
                .HasForeignKey(userSkill => userSkill.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
