using feedbackFlowAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace feedbackFlowAPI.Helpers.Configuration
{
    public class ClassConfiguration : IEntityTypeConfiguration<Class>
    {
        public void Configure(EntityTypeBuilder<Class> entity)
        {
            entity.HasKey(e => e.Id).HasName("classes_pkey");

            entity.ToTable("classes");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CourseId)
                .HasColumnName("course_id");
            entity.Property(e => e.DeletedAt)
                .HasColumnName("deleted_at");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Year)
                .HasColumnName("year");
            entity.Property(e => e.Education)
                .HasColumnType("education")
                .HasColumnName("education");
            entity.Property(e => e.ClassLevel)
                .HasColumnType("class_level")
                .HasColumnName("class_level");

            entity.HasOne(d => d.Teacher).WithMany(p => p.TeacherClasses)
                .HasForeignKey(d => d.TeacherId)
                .HasConstraintName("fk_teacher_to_classes");

            entity.HasOne(d => d.Course).WithMany(p => p.Classes)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("fk_courses_to_classes");
        }
    }
}
