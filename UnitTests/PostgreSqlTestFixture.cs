using Bogus;
using DotNet.Testcontainers.Builders;
using feedbackFlowAPI.Entities;
using feedbackFlowAPI.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Testcontainers.PostgreSql;

namespace UnitTests
{
    public class PostgreSqlTestFixture : IAsyncLifetime
    {
        public PostgreSqlContainer Container { get; private set; } = null!;
        public DbContextOptions<FbfDbContext> DbContextOptions { get; set; } = null!;

        public async ValueTask InitializeAsync()
        {
            Container = new PostgreSqlBuilder()
                .WithImage("postgres:18.3")
                .WithDatabase("testdb")
                .WithUsername("postgres")
                .WithPassword("postgres")
                .WithCleanUp(true)
                .Build();

            await Container.StartAsync();

            var connectionString = Container.GetConnectionString();
            var optionsBuilder = new DbContextOptionsBuilder<FbfDbContext>();
            optionsBuilder.UseNpgsql(
                connectionString,
                npgsqlOptionsAction: o =>
                {
                    o.MapEnum<ClassLevel>("class_level");
                    o.MapEnum<ExamType>("exam_type");
                    o.MapEnum<Visibility>("visibility");
                    o.MapEnum<QuestionDifficulty>("question_difficulty");
                    o.MapEnum<QuestionMethodRequirement>("question_method_requirement");
                    o.MapEnum<Education>("education");
                    o.MapEnum<QuestionContext>("question_context");
                    o.MapEnum<StandardQuestion>("standard_question");
                    o.MapEnum<NewOldSystem>("new_old_system");
                });
            DbContextOptions = optionsBuilder.Options;

            using var context = new FbfDbContext(DbContextOptions);
            await context.Database.MigrateAsync();
            await TestDataSeeder.SeedAsync(context);
        }

        public async ValueTask DisposeAsync()
        {
            await Container.DisposeAsync();
        }
    }
}
