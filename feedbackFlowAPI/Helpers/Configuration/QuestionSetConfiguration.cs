using feedbackFlowAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace feedbackFlowAPI.Helpers.Configuration
{
    public class QuestionSetConfiguration : IEntityTypeConfiguration<QuestionSet>
    {
        public void Configure(EntityTypeBuilder<QuestionSet> entity)
        {
            entity.HasKey(e => e.Id).HasName("question_sets_pkey");

            entity.ToTable("question_sets");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.IsDeleted)
                .HasColumnName("is_deleted");
            entity.Property(e => e.IsDraft)
                .HasComment("if its assigned to students")
                .HasColumnName("is_draft");
            entity.Property(e => e.IsExam)
                .HasColumnName("is_exam");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");

            entity.HasOne(q => q.Teacher)
                .WithMany(u => u.QuestionSets)
                .HasForeignKey(q => q.TeacherId)
                .HasConstraintName("fk_teacher_to_questionset");
        }
    }
}
