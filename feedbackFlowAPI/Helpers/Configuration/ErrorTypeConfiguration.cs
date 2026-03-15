using feedbackFlowAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace feedbackFlowAPI.Helpers.Configuration
{
    public class ErrorTypeConfiguration : IEntityTypeConfiguration<ErrorType>
    {
        public void Configure(EntityTypeBuilder<ErrorType> entity)
        {
            entity.HasKey(e => e.Id).HasName("error_types_pkey");

            entity.ToTable("error_types");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("error_type");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.UserId)
                .HasColumnName("user_id");

            entity.HasOne(d => d.User)
                .WithMany(p => p.ErrorTypes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_users_to_errortypes");
        }
    }
}
