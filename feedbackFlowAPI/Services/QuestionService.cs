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
            var errors = ValidateQuestionDto(questionDto);
            if (errors.Count > 0)
            {
                return (null, errors);
            }

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

            return (question, errors);
        }

        public async Task<bool> UpdateQuestionAsync(Question question)
        {
            _context.Entry(question).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                var exists = await _context.Questions.AnyAsync(e => e.Id == question.Id);
                if (!exists)
                {
                    return false;
                }

                throw;
            }
        }

        public async Task<bool> DeleteQuestionAsync(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return false;
            }

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();

            return true;
        }

        private static List<string> ValidateQuestionDto(QuestionDTO questionDto)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(questionDto.ImgSrc))
            {
                errors.Add("ImgSrc is required.");
            }

            if (string.IsNullOrWhiteSpace(questionDto.Points))
            {
                errors.Add("Points is required.");
            }

            if (!questionDto.ExamType.HasValue)
            {
                errors.Add("ExamType is required.");
            }

            if (!questionDto.ClassLevel.HasValue)
            {
                errors.Add("ClassLevel is required.");
            }

            if (!questionDto.QuestionDifficulty.HasValue)
            {
                errors.Add("QuestionDifficulty is required.");
            }

            if (!questionDto.QuestionMethodRequirement.HasValue)
            {
                errors.Add("QuestionMethodRequirement is required.");
            }

            if (!questionDto.Education.HasValue)
            {
                errors.Add("Education is required.");
            }

            if (!questionDto.QuestionContext.HasValue)
            {
                errors.Add("QuestionContext is required.");
            }

            if (!questionDto.StandardQuestion.HasValue)
            {
                errors.Add("StandardQuestion is required.");
            }

            if (!questionDto.NewOldSystem.HasValue)
            {
                errors.Add("NewOldSystem is required.");
            }

            if (!questionDto.UserId.HasValue || questionDto.UserId.Value <= 0)
            {
                errors.Add("UserId is required and must be greater than 0.");
            }

            if (questionDto.CourseId.HasValue && questionDto.CourseId.Value <= 0)
            {
                errors.Add("CourseId must be greater than 0 when provided.");
            }

            return errors;
        }
    }
}
