using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.Entities;
using feedbackFlowAPI.Helpers;
using feedbackFlowAPI.Helpers.ControllerHelpers;
using feedbackFlowAPI.Mappers.Interface;
using feedbackFlowAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace feedbackFlowAPI.Services.Implementations
{
    public class QuestionService : IQuestionService
    {
        private readonly FbfDbContext _context;
        private readonly IQuestionMapper _mapper;

        public QuestionService(FbfDbContext context, IQuestionMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GetQuestionResponse> GetQuestions(GetQuestionRequest getQuestionRequest)
        {
            List<QuestionDTO> questions = await _context.Questions
                .AsNoTracking()
                .OrderByDescending(q => q.Id)
                .Where(q => q.DeletedAt == null)
                .Where(q => q.Id < getQuestionRequest.LastId)
                .Take(getQuestionRequest.PageSize)
                .Select(q => _mapper.ToDTO(q))
                .ToListAsync();

            GetQuestionResponse getQuestionResponse = new GetQuestionResponse
            {
                Questions = questions,
                LastId = questions.Any() ? questions.Last().Id : null
            };

            return getQuestionResponse;
        }

        public async Task<Question?> GetQuestionByIdAsync(int id)
        {
            return await _context.Questions
                .AsNoTracking()
                .FirstOrDefaultAsync(q => q.Id == id && q.DeletedAt == null);
        }

        public async Task<Question> CreateQuestionAsync(QuestionDTO questionDto)
        {
            var question = new Question
            {
                ImgSrc = questionDto.ImgSrc.Trim(),
                Points = questionDto.Points.Trim(),
                DeletedAt = null,
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

            return question;
        }

        public async Task<bool> UpdateQuestionAsync(int id, QuestionDTO questionDto)
        {
            var question = await _context.Questions.FirstOrDefaultAsync(q => q.Id == id && q.DeletedAt == null);
            if (question == null)
            {
                return false;
            }

            question.ImgSrc = questionDto.ImgSrc.Trim();
            question.Points = questionDto.Points.Trim();
            question.ExamType = questionDto.ExamType!.Value;
            question.ClassLevel = questionDto.ClassLevel!.Value;
            question.QuestionDifficulty = questionDto.QuestionDifficulty!.Value;
            question.QuestionMethodRequirement = questionDto.QuestionMethodRequirement!.Value;
            question.Education = questionDto.Education!.Value;
            question.QuestionContext = questionDto.QuestionContext!.Value;
            question.StandardQuestion = questionDto.StandardQuestion!.Value;
            question.NewOldSystem = questionDto.NewOldSystem!.Value;
            question.CourseId = questionDto.CourseId;
            question.UserId = questionDto.UserId!.Value;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteQuestionAsync(int id)
        {
            var question = await _context.Questions.FirstOrDefaultAsync(q => q.Id == id && q.DeletedAt == null);
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
