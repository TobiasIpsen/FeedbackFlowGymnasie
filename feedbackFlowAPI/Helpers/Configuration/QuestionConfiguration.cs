using feedbackFlowAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace feedbackFlowAPI.Helpers.Configuration
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> entity)
        {
            entity.HasKey(e => e.Id).HasName("questions_pkey");

            entity.ToTable("questions");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.ImgSrc)
                .HasColumnName("img_src")
                .HasColumnType("character varying");
            entity.Property(e => e.Points)
                .HasColumnType("character varying")
                .HasColumnName("points");
            entity.Property(e => e.DeletedAt)
                .HasColumnName("deleted_at");
            entity.Property(e => e.CourseId)
                .HasColumnName("course_id");
            entity.Property(e => e.UserId)
                .HasColumnName("user_id");

            entity.Property(e => e.ExamType)
                .HasColumnType("exam_type")
                .HasColumnName("exam_type");
            entity.Property(e => e.ClassLevel)
                .HasColumnType("class_level")
                .HasColumnName("class_level");
            entity.Property(e => e.QuestionDifficulty)
                .HasColumnType("question_difficulty")
                .HasColumnName("question_difficulty");
            entity.Property(e => e.QuestionMethodRequirement)
                .HasColumnType("question_method_requirement")
                .HasColumnName("question_method_requirement");
            entity.Property(e => e.Education)
                .HasColumnType("education")
                .HasColumnName("education");
            entity.Property(e => e.QuestionContext)
                .HasColumnType("question_context")
                .HasColumnName("question_context");
            entity.Property(e => e.StandardQuestion)
                .HasColumnType("standard_question")
                .HasColumnName("standard_question");
            entity.Property(e => e.NewOldSystem)
                .HasColumnType("new_old_system")
                .HasColumnName("new_old_system");

            entity.HasOne(d => d.Course)
                .WithMany(p => p.Questions)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_courses_to_questions");

            entity.HasOne(d => d.User)
                .WithMany(p => p.Questions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_users_to_questions");
        }
    }
}
