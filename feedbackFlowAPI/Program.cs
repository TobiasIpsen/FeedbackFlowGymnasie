
using feedbackFlowAPI.Helpers;
using Microsoft.EntityFrameworkCore;
using feedbackFlowAPI.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace feedbackFlowAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
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
                        o.MapEnum<UserRole>("user_role");
                        o.MapEnum<ClassLevel>("class_level");
                        o.MapEnum<QuestionType>("question_type");
                        o.MapEnum<Visibility>("visibility");
                    });
            });

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

            app.MapControllers();

            app.Run();
        }
    }
}
