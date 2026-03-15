using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace feedbackFlowAPI.Helpers.Configuration
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> entity)
        {
            entity.HasKey(e => e.Name).HasName("userroles_pkey");

            entity.ToTable("user_roles");

            entity.HasData(
                new UserRole { Name = "Admin" },
                new UserRole { Name = "Teacher" },
                new UserRole { Name = "Student" }
            );
        }
    }
}
