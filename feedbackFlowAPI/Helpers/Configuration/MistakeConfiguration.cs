using feedbackFlowAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace feedbackFlowAPI.Helpers.Configuration
{
    public class MistakeConfiguration : IEntityTypeConfiguration<Mistake>
    {
        public void Configure(EntityTypeBuilder<Mistake> entity)
        {
            entity.HasKey(e => e.Id).HasName("mistakes_pkey");

            entity.ToTable("mistakes");

            entity.Property(e => e.Id)
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");

            entity.HasData(
                new Mistake { Id = 1, Name = "Presentation" },
                new Mistake { Id = 2, Name = "Documentation" },
                new Mistake { Id = 3, Name = "Argumentation" },
                new Mistake { Id = 4, Name = "Conclusion" },
                new Mistake { Id = 5, Name = "Calculation" },
                new Mistake { Id = 6, Name = "Insertion" },
                new Mistake { Id = 7, Name = "Inaccurate" },
                new Mistake { Id = 8, Name = "Technical" },
                new Mistake { Id = 9, Name = "Misunderstanding" }
            );
        }
    }
}
