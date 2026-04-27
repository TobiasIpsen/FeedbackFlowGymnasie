using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.Helpers;
using feedbackFlowAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace feedbackFlowAPI.Services.Implementations
{
    public class QuestionService : IQuestionService
    {
        private readonly FbfDbContext _context;

        public QuestionService(FbfDbContext context)
        {
            _context = context;
        }

        public async Task<List<QuestionDTO>> FilterQuestions(QuestionFilterDTO filter)
        {
            var query = _context.Questions
                .AsNoTracking()
                .Where(q => q.DeletedAt == null);

            if (filter.ClassLevel.HasValue)
            {
                query = query.Where(q => q.ClassLevel == filter.ClassLevel.Value);
            }

            if (filter.Education.HasValue)
            {
                query = query.Where(q => q.Education == filter.Education.Value);
            }

            if (filter.ExamType.HasValue)
            {
                query = query.Where(q => q.ExamType == filter.ExamType.Value);
            }

            if (filter.QuestionDifficulty.HasValue)
            {
                query = query.Where(q => q.QuestionDifficulty == filter.QuestionDifficulty.Value);
            }

            if (filter.SubjectId.HasValue)
            {
                var subjectId = filter.SubjectId.Value;
                query = query.Where(q => _context.QuestionQuestionSets.Any(qqs => qqs.QuestionId == q.Id && qqs.SubjectId == subjectId));
            }

            if (!string.IsNullOrWhiteSpace(filter.Subject))
            {
                var subjectTerm = filter.Subject.Trim();
                query = query.Where(q => _context.QuestionQuestionSets.Any(qqs =>
                    qqs.QuestionId == q.Id &&
                    EF.Functions.ILike(qqs.Subject.Name, $"%{subjectTerm}%")));
            }

            if (filter.Year.HasValue)
            {
                var year = filter.Year.Value;
                query = query.Where(q =>
                    q.CourseId.HasValue &&
                    _context.Classes.Any(c =>
                        c.CourseId == q.CourseId.Value &&
                        c.DeletedAt == null &&
                        c.Year.Year == year));
            }

            if (!string.IsNullOrWhiteSpace(filter.ClassName))
            {
                var className = filter.ClassName.Trim();
                query = query.Where(q =>
                    q.CourseId.HasValue &&
                    _context.Classes.Any(c =>
                        c.CourseId == q.CourseId.Value &&
                        c.DeletedAt == null &&
                        EF.Functions.ILike(c.Name, $"%{className}%")));
            }

            if (!string.IsNullOrWhiteSpace(filter.FreeText))
            {
                var term = filter.FreeText.Trim();
                query = query.Where(q =>
                    EF.Functions.ILike(q.Points, $"%{term}%") ||
                    EF.Functions.ILike(q.ImgSrc, $"%{term}%") ||
                    _context.QuestionQuestionSets.Any(qqs => qqs.QuestionId == q.Id && EF.Functions.ILike(qqs.Subject.Name, $"%{term}%")));
            }

            return await query
                .OrderBy(q => q.Id)
                .Select(q => new QuestionDTO
                {
                    ImgSrc = q.ImgSrc,
                    Points = q.Points,
                    DeletedAt = q.DeletedAt,
                    ExamType = q.ExamType,
                    ClassLevel = q.ClassLevel,
                    QuestionDifficulty = q.QuestionDifficulty,
                    QuestionMethodRequirement = q.QuestionMethodRequirement,
                    Education = q.Education,
                    StandardQuestion = q.StandardQuestion,
                    Title = BuildTitle(q.Points),
                    Description = BuildDescription(q.Points)
                })
                .ToListAsync();
        }

        private static string BuildTitle(string points)
        {
            if (string.IsNullOrWhiteSpace(points))
            {
                return "Untitled question";
            }

            var oneLine = points.Replace("\r", " ").Replace("\n", " ").Trim();
            return oneLine.Length <= 70 ? oneLine : oneLine[..70] + "...";
        }

        private static string BuildDescription(string points)
        {
            if (string.IsNullOrWhiteSpace(points))
            {
                return string.Empty;
            }

            var oneLine = points.Replace("\r", " ").Replace("\n", " ").Trim();
            return oneLine.Length <= 180 ? oneLine : oneLine[..180] + "...";
        }
    }
}
