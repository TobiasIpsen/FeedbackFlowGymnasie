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
                new Subject { Id = 1, Name = "Combinatorics" },
                new Subject { Id = 2, Name = "Differential Calculus" },
                new Subject { Id = 3, Name = "Quadratic polynomial" },
                new Subject { Id = 4, Name = "Regression" },
                new Subject { Id = 5, Name = "Exponential function" },
                new Subject { Id = 6, Name = "Binomial distribution" }
            );
        }
    }
}
