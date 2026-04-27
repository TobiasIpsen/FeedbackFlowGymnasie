using feedbackFlowAPI.DTOs;

namespace feedbackFlowAPI.Services.Interfaces
{
    public interface IQuestionService
    {
        Task<List<QuestionDTO>> FilterQuestions(QuestionFilterDTO filter);
    }
}
