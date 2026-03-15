using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using feedbackFlowAPI.Entities;
using feedbackFlowAPI.Helpers.Configuration;

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

    public virtual DbSet<QuestionQuestionSet> QuestionQuestionSets { get; set; }

    public virtual DbSet<StudentResult> StudentResults { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum<ClassLevel>()
            .HasPostgresEnum<ExamType>()
            .HasPostgresEnum<Visibility>()
            .HasPostgresEnum<QuestionDifficulty>()
            .HasPostgresEnum<QuestionMethodRequirement>()
            .HasPostgresEnum<Education>()
            .HasPostgresEnum<QuestionContext>()
            .HasPostgresEnum<StandardQuestion>()
            .HasPostgresEnum<NewOldSystem>();

        modelBuilder.ApplyConfiguration(new AppendixConfiguration());
        modelBuilder.ApplyConfiguration(new ClassConfiguration());
        modelBuilder.ApplyConfiguration(new ContentTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CourseConfiguration());
        modelBuilder.ApplyConfiguration(new ErrorTypeConfiguration());
        modelBuilder.ApplyConfiguration(new QuestionConfiguration());
        modelBuilder.ApplyConfiguration(new QuestionAnswerConfiguration());
        modelBuilder.ApplyConfiguration(new QuestionCollectionConfiguration());
        modelBuilder.ApplyConfiguration(new QuestionSetConfiguration());
        modelBuilder.ApplyConfiguration(new QuestionQuestionSetConfiguration());
        modelBuilder.ApplyConfiguration(new StudentResultConfiguration());
        modelBuilder.ApplyConfiguration(new SubjectConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
