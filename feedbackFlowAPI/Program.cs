
using feedbackFlowAPI.Helpers;
using feedbackFlowAPI.Interface;
using feedbackFlowAPI.Services;
using Microsoft.EntityFrameworkCore;
using feedbackFlowAPI.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using System.Text.Json.Serialization;
using System.Text.Json;
using feedbackFlowAPI.Services.Interfaces;
using feedbackFlowAPI.Services.Implementations;
using feedbackFlowAPI.Mappers.Implementations;
using feedbackFlowAPI.Mappers.Interface;

namespace feedbackFlowAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSingleton<IStudentResultMapper, StudentResultMapper>();
            builder.Services.AddScoped<IStudentResultsService, StudentResultsService>();
            builder.Services.AddSingleton<IClassMapper, ClassMapper>();
            builder.Services.AddScoped<IClassService, ClassService>();
            builder.Services.AddSingleton<IQuestionSetMapper, QuestionSetMapper>();
            builder.Services.AddScoped<IQuestionSetService, QuestionSetService>();
            builder.Services.AddSingleton<IQuestionMapper, QuestionMapper>();
            builder.Services.AddScoped<IQuestionService, QuestionService>();

            builder.Services
                .AddControllers()
                .AddJsonOptions(o =>
                {
                    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
                    o.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                });

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<FbfDbContext>(options =>
            {
                options.UseNpgsql(
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
            });

                    builder.Services.AddScoped<IQuestionService, QuestionService>();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapGet("/", () => Results.Ok(new
            {
                message = "FeedbackFlow API kører",
                health = "/health",
                users = "/api/users",
                openApi = "/openapi/v1.json"
            }));

            app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

            app.MapControllers();

            app.Run();
        }
    }
}
