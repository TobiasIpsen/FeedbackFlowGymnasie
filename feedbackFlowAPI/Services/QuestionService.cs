using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.Helpers;
using feedbackFlowAPI.Helpers.ControllerHelpers;
using feedbackFlowAPI.Services.Interfaces;
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

        public async Task<GetQuestionResponse> GetQuestions(GetQuestionRequest getQuestionRequest)
        {
            var questions = await _context.Questions
                .AsNoTracking()
                .OrderByDescending(q => q.Id)
                .Where(q => q.DeletedAt == null)
                .Where(q => q.Id < getQuestionRequest.LastId)
                .Take(getQuestionRequest.PageSize)
                .Select(q => new QuestionDTO
                {
                    Id = q.Id,
                    ImgSrc = q.ImgSrc,
                    Points = q.Points,
                    DeletedAt = q.DeletedAt,
                    ExamType = q.ExamType,
                    ClassLevel = q.ClassLevel,
                    QuestionDifficulty = q.QuestionDifficulty,
                    QuestionMethodRequirement = q.QuestionMethodRequirement,
                    Education = q.Education,
                    QuestionContext = q.QuestionContext,
                    StandardQuestion = q.StandardQuestion,
                    NewOldSystem = q.NewOldSystem
                })
                .ToListAsync();

            return new GetQuestionResponse
            {
                Questions = questions,
                LastId = questions.Any() ? questions.Last().Id : null
            };
        }
    }
}
