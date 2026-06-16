using feedbackFlowAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace feedbackFlowAPI.Helpers.Configuration
{
    public class StudentResultConfiguration : IEntityTypeConfiguration<StudentResult>
    {
        public void Configure(EntityTypeBuilder<StudentResult> entity)
        {
            entity.HasKey(e => e.Id).HasName("student_results_pkey");

            entity.ToTable("student_results");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.TeacherPoint)
                .HasColumnName("teacher_point");
            entity.Property(e => e.TeacherFeedback)
                .HasColumnType("character varying")
                .HasColumnName("teacher_feedback");
            entity.Property(e => e.StudentSelfAssessmentPoints)
                .HasColumnName("student_self_assessment_points");
            entity.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("NOW()");
            entity.Property(e => e.DeletedAt)
                .HasColumnName("deleted_at");
            entity.Property(e => e.TeacherId)
                .HasColumnName("teacher_id");
            entity.Property(e => e.StudentId)
                .HasColumnName("student_id");
            entity.Property(e => e.QuestionId)
                .HasColumnName("question_id");
            entity.Property(e => e.QuestionSetId)
                .HasColumnName("question_set_id");

            entity.HasOne(d => d.Question)
                .WithMany(p => p.StudentResults)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_questions_to_studentresults");

            entity.HasOne(d => d.QuestionSet)
                .WithMany(p => p.StudentResults)
                .HasForeignKey(d => d.QuestionSetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_questionsets_to_studentresults");

            entity.HasOne(sr => sr.Student)
                .WithMany(u => u.StudentResults)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_student_to_studentresults");

            entity.HasOne(d => d.Teacher)
                .WithMany(u => u.TeacherStudentResults)
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_teacher_to_studentresults");
        }
    }
}
