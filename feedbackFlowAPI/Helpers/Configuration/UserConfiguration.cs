using feedbackFlowAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace feedbackFlowAPI.Helpers.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasColumnType("character varying")
                .HasColumnName("first_name");
            entity.Property(e => e.IsDeleted).
                HasColumnName("is_deleted");
            entity.Property(e => e.Lastname)
                .HasColumnType("character varying")
                .HasColumnName("last_name");
            entity.Property(e => e.UserRole)
                .HasColumnType("user_role")
                .HasColumnName("user_role");
        }
    }
}
