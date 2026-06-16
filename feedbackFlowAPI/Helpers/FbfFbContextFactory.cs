using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace feedbackFlowAPI.Helpers
{
    public class FbfDbContextFactory : IDesignTimeDbContextFactory<FbfDbContext>
    {
        public FbfDbContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var builder = new DbContextOptionsBuilder<FbfDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseNpgsql(
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

            return new FbfDbContext(builder.Options);
        }
    }

}
