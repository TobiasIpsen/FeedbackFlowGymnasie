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

    public DbSet<Appendix> Appendices { get; set; }

    public DbSet<Class> Classes { get; set; }

    public DbSet<ContentType> ContentTypes { get; set; }

    public DbSet<Course> Courses { get; set; }

    public DbSet<ErrorType> Errortypes { get; set; }

    public DbSet<Question> Questions { get; set; }

    public DbSet<QuestionAnswer> QuestionAnswers { get; set; }

    public DbSet<QuestionCollection> QuestionCollections { get; set; }

    public DbSet<QuestionSet> QuestionSets { get; set; }

    public DbSet<QuestionQuestionSet> QuestionQuestionSets { get; set; }

    public DbSet<StudentResult> StudentResults { get; set; }

    public DbSet<Subject> Subjects { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<UserRole> UserRoles { get; set; }

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
