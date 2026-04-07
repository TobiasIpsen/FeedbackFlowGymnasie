using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.Entities;

namespace feedbackFlowAPI.Interface
{
    public interface IQuestionService
    {
        Task<List<Question>> GetQuestionsAsync();
        Task<Question?> GetQuestionByIdAsync(int id);
        Task<(Question? Question, List<string> Errors)> CreateQuestionAsync(QuestionDTO questionDto);
        Task<bool> UpdateQuestionAsync(Question question);
        Task<bool> DeleteQuestionAsync(int id);
    }
}
