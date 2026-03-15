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
            entity.Property(e => e.IsDeleted)
                .HasColumnName("is_deleted");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");

            entity.HasData(
                new ContentType { Name = "Text" },
                new ContentType { Name = "Algebraic" },
                new ContentType { Name = "Graph" },
                new ContentType { Name = "Figure" },
                new ContentType { Name = "Table" }
            );
        }
    }
}
