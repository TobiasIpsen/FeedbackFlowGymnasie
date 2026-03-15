using feedbackFlowAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace feedbackFlowAPI.Helpers.Configuration
{
    public class QuestionQuestionSetConfiguration : IEntityTypeConfiguration<QuestionQuestionSet>
    {
        public void Configure(EntityTypeBuilder<QuestionQuestionSet> entity)
        {
            entity.ToTable("questions_question_sets");

            entity.HasKey(qqs => new { qqs.QuestionId, qqs.QuestionSetId, qqs.SubjectId });

            entity.Property(e => e.SubjectId)
                .HasColumnName("subject_id");
            entity.Property(e => e.QuestionId)
                .HasColumnName("question_id");
            entity.Property(e => e.QuestionSetId)
                .HasColumnName("question_set_id");

            entity.HasOne(d => d.Question)
                .WithMany(p => p.QuestionSet)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("fk_questions_to_questionquestionset");
            entity.HasOne(d => d.QuestionSet)
                .WithMany(p => p.Question)
                .HasForeignKey(d => d.QuestionSetId)
                .HasConstraintName("fk_questionsets_to_questionquestionset");
            entity.HasOne(d => d.Subject)
                .WithMany(p => p.QuestionQuestionSet)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("fk_subject_to_questionquestionset");
        }
    }
}
