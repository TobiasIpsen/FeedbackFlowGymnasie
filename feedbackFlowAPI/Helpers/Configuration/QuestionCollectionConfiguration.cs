using feedbackFlowAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace feedbackFlowAPI.Helpers.Configuration
{
    public class QuestionCollectionConfiguration : IEntityTypeConfiguration<QuestionCollection>
    {
        public void Configure(EntityTypeBuilder<QuestionCollection> entity)
        {
            entity.HasKey(e => e.Id).HasName("question_collections_pkey");

            entity.ToTable("question_collections");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Points)
                .HasColumnType("character varying")
                .HasColumnName("points");
            entity.Property(e => e.QuestionId)
                .HasColumnName("question_id");
            entity.Property(e => e.Sequence)
                .HasColumnName("sequence");

            entity.HasOne(d => d.Question)
                .WithMany(p => p.QuestionCollections)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_questions_to_questioncollections");
        }
    }
}
