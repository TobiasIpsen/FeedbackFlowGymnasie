using feedbackFlowAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace feedbackFlowAPI.Helpers.Configuration
{
    public class ContentTypeConfiguration : IEntityTypeConfiguration<ContentType>
    {
        public void Configure(EntityTypeBuilder<ContentType> entity)
        {
            entity.HasKey(e => e.Id).HasName("contenttype_pkey");

            entity.ToTable("content_types", tb => tb.HasComment("(diagram, tekst)"));

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.DeletedAt)
                .HasColumnName("deleted_at");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");

            entity.HasData(
                new ContentType { Id = 1, Name = "Text" },
                new ContentType { Id = 2, Name = "Algebraic" },
                new ContentType { Id = 3, Name = "Graph" },
                new ContentType { Id = 4, Name = "Figure" },
                new ContentType { Id = 5, Name = "Table" }
            );
        }
    }
}
