using feedbackFlowAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace feedbackFlowAPI.Helpers.Configuration
{
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> entity)
        {
            entity.HasKey(e => e.Id).HasName("subjects_pkey");

            entity.ToTable("subjects", tb => tb.HasComment("trigonometri, vectors"));

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("subject");

            entity.HasData(
                new Subject { Name = "Combinatorics" },
                new Subject { Name = "Differential Calculus" },
                new Subject { Name = "Quadratic polynomial" },
                new Subject { Name = "Regression" },
                new Subject { Name = "Exponential function" },
                new Subject { Name = "Binomial distribution" }
            )
        }
    }
}
