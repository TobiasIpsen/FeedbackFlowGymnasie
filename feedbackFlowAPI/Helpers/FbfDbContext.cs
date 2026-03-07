using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using feedbackFlowAPI.Entities;

namespace feedbackFlowAPI.Helpers;

public partial class FbfDbContext : DbContext
{

    public FbfDbContext(DbContextOptions<FbfDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appendix> Appendices { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<ContentType> ContentTypes { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<ErrorType> Errortypes { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<QuestionAnswer> QuestionAnswers { get; set; }

    public virtual DbSet<QuestionCollection> QuestionCollections { get; set; }

    public virtual DbSet<QuestionSet> QuestionSets { get; set; }

    public virtual DbSet<StudentResult> StudentResults { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum<UserRole>()
            .HasPostgresEnum<ClassLevel>()
            .HasPostgresEnum<QuestionType>()
            .HasPostgresEnum<Visibility>();

        modelBuilder.Entity<Appendix>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("appendix_pkey");

            entity.ToTable("appendix", tb => tb.HasComment("(Billag)"));

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false);
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.QuestionId).HasColumnName("question_id");
            entity.Property(e => e.Url)
                .HasColumnType("character varying")
                .HasColumnName("url");

            entity.HasOne(d => d.Question).WithMany(p => p.Appendices)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_questions_to_appendix");
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("classes_pkey");

            entity.ToTable("classes");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false);
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Year).HasColumnName("year");
            entity.Property(e => e.ClassLevel)
                .HasColumnType("class_level")
                .HasColumnName("class_level");

            entity.HasOne(d => d.Course).WithMany(p => p.Classes)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_courses_to_classes");
        });

        modelBuilder.Entity<ContentType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("contenttype_pkey");

            entity.ToTable("content_type", tb => tb.HasComment("(diagram, tekst)"));

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false);
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("courses_pkey");

            entity.ToTable("courses");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasComment("mat, fys, etc.")
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<ErrorType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("errortypes_pkey");

            entity.ToTable("error_types");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("error_type");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.ErrorTypes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_users_to_errortypes");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("questions_pkey");

            entity.ToTable("questions");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false);
            entity.Property(e => e.Points)
                .HasColumnType("character varying")
                .HasColumnName("points");
            entity.Property(e => e.QuestionId).HasColumnName("question_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.QuestionType)
                .HasColumnType("question_type")
                .HasColumnName("question_type");
            entity.Property(e => e.ClassLevel)
                .HasColumnType("class_level")
                .HasColumnName("class_level");

            entity.HasOne(d => d.Course).WithMany(p => p.Questions)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_courses_to_questions");

            entity.HasOne(d => d.QuestionNavigation).WithMany(p => p.InverseQuestionNavigation)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("fk_questions_to_questions");

            entity.HasOne(d => d.User).WithMany(p => p.Questions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_users_to_questions");
        });

        modelBuilder.Entity<QuestionAnswer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("questionanswers_pkey");

            entity.ToTable("question_answers");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false);
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.QuestionId).HasColumnName("question_id");
            entity.Property(e => e.QuestionSetId)
                .HasComment("f.feks. svar til hele questionset i stedet for kun 1 question")
                .HasColumnName("question_set_id");
            entity.Property(e => e.Url)
                .HasColumnType("character varying")
                .HasColumnName("url");
            entity.Property(e => e.Visibility)
                .HasColumnType("visibility")
                .HasColumnName("visibility");

            entity.HasOne(d => d.Question).WithMany(p => p.QuestionAnswers)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_questions_to_questionanswers");

            entity.HasOne(d => d.QuestionSet).WithMany(p => p.QuestionAnswers)
                .HasForeignKey(d => d.QuestionSetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_questionsets_to_questionanswers");
        });

        modelBuilder.Entity<QuestionCollection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("questioncollections_pkey");

            entity.ToTable("questioncollections");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Points)
                .HasColumnType("character varying")
                .HasColumnName("points");
            entity.Property(e => e.QuestionId).HasColumnName("question_id");
            entity.Property(e => e.Sequence).HasColumnName("sequence");

            entity.HasOne(d => d.Question).WithMany(p => p.QuestionCollections)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_questions_to_questioncollections");
        });

        modelBuilder.Entity<QuestionSet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("questionsets_pkey");

            entity.ToTable("questionsets");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false);
            entity.Property(e => e.IsDraft)
                .HasComment("if its assigned to students")
                .HasColumnName("is_draft");
            entity.Property(e => e.IsExam).HasColumnName("is_exam");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<StudentResult>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("studentresults_pkey");

            entity.ToTable("StudentResults");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CreatedDate).HasColumnName("createddate");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false);
            entity.Property(e => e.QuestionId).HasColumnName("question_id");
            entity.Property(e => e.QuestionSetId).HasColumnName("question_set_id");
            entity.Property(e => e.StudentSelfAssessmentPoints)
                .HasColumnType("character varying")
                .HasColumnName("student_self_assessment_points");
            entity.Property(e => e.TeacherFeedback)
                .HasColumnType("character varying")
                .HasColumnName("teacher_feedback");
            entity.Property(e => e.TeacherPoint)
                .HasColumnType("character varying")
                .HasColumnName("teacher_point");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Question).WithMany(p => p.StudentResults)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_questions_to_studentresults");

            entity.HasOne(d => d.QuestionSet).WithMany(p => p.StudentResults)
                .HasForeignKey(d => d.QuestionSetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_questionsets_to_studentresults");

            entity.HasOne(d => d.User).WithMany(p => p.StudentResults)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_users_to_studentresults");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("subjects_pkey");

            entity.ToTable("subjects");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.name)
                .HasComment("trigonometri, vectors")
                .HasColumnType("character varying")
                .HasColumnName("subject");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasColumnType("character varying")
                .HasColumnName("firstname");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false);
            entity.Property(e => e.Lastname)
                .HasColumnType("character varying")
                .HasColumnName("lastname");
            entity.Property(e => e.UserRole)
                .HasColumnType("user_role")
                .HasColumnName("user_role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
