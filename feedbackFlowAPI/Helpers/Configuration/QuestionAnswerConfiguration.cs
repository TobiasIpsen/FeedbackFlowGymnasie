using feedbackFlowAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace feedbackFlowAPI.Helpers.Configuration
{
    public class QuestionAnswerConfiguration : IEntityTypeConfiguration<QuestionAnswer>
    {
        public void Configure(EntityTypeBuilder<QuestionAnswer> entity)
        {
            entity.HasKey(e => e.Id).HasName("question_answers_pkey");

            entity.ToTable("question_answers");

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
            entity.Property(e => e.QuestionSetId)
                .HasComment("f.feks. svar til hele questionset i stedet for kun 1 question")
                .HasColumnName("question_set_id");
            entity.Property(e => e.Url)
                .HasColumnType("character varying")
                .HasColumnName("url");
            entity.Property(e => e.Visibility)
                .HasColumnType("visibility")
                .HasColumnName("visibility");

            entity.HasOne(d => d.Question)
                .WithMany(p => p.QuestionAnswers)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_questions_to_questionanswers");

            entity.HasOne(d => d.QuestionSet)
                .WithMany(p => p.QuestionAnswers)
                .HasForeignKey(d => d.QuestionSetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_questionsets_to_questionanswers");
        }
    }
}
