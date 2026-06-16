using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.Entities;
using feedbackFlowAPI.Helpers;
using feedbackFlowAPI.Interface;
using Microsoft.EntityFrameworkCore;

namespace feedbackFlowAPI.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly FbfDbContext _context;

        public QuestionService(FbfDbContext context)
        {
            _context = context;
        }

        public async Task<List<Question>> GetQuestionsAsync()
        {
            return await _context.Questions.ToListAsync();
        }

        public async Task<Question?> GetQuestionByIdAsync(int id)
        {
            return await _context.Questions.FindAsync(id);
        }

        public async Task<(Question? Question, List<string> Errors)> CreateQuestionAsync(QuestionDTO questionDto)
        {
            var question = new Question
            {
                ImgSrc = questionDto.ImgSrc.Trim(),
                Points = questionDto.Points.Trim(),
                DeletedAt = questionDto.DeletedAt,
                ExamType = questionDto.ExamType!.Value,
                ClassLevel = questionDto.ClassLevel!.Value,
                QuestionDifficulty = questionDto.QuestionDifficulty!.Value,
                QuestionMethodRequirement = questionDto.QuestionMethodRequirement!.Value,
                Education = questionDto.Education!.Value,
                QuestionContext = questionDto.QuestionContext!.Value,
                StandardQuestion = questionDto.StandardQuestion!.Value,
                NewOldSystem = questionDto.NewOldSystem!.Value,
                CourseId = questionDto.CourseId,
                UserId = questionDto.UserId!.Value
            };

            _context.Questions.Add(question);
            await _context.SaveChangesAsync();

            return (question, new List<string>());
        }

        public async Task<bool> DeleteQuestionAsync(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return false;
            }

            question.DeletedAt = DateTimeOffset.UtcNow;
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
