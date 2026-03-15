using feedbackFlowAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace feedbackFlowAPI.Helpers.Configuration
{
    public class AppendixConfiguration : IEntityTypeConfiguration<Appendix>
    {
        public void Configure(EntityTypeBuilder<Appendix> entity)
        {
            entity.HasKey(e => e.Id).HasName("appendix_pkey");

            entity.ToTable("appendix", tb => tb.HasComment("(Billag)"));

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.IsDeleted)
                .HasColumnName("is_deleted");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.QuestionId)
                .HasColumnName("question_id");
            entity.Property(e => e.Url)
                .HasColumnType("character varying")
                .HasColumnName("url");

            entity.HasOne(d => d.Question).WithMany(p => p.Appendices)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_questions_to_appendix");
        }
    }
}
