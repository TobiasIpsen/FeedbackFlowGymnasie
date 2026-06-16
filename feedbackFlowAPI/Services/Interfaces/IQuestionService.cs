using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.Entities;
using feedbackFlowAPI.Helpers.ControllerHelpers;

namespace feedbackFlowAPI.Services.Interfaces
{
    public interface IQuestionService
    {
        Task<GetQuestionResponse> GetQuestions(GetQuestionRequest getQuestionRequest);

        Task<Question?> GetQuestionByIdAsync(int id);

        Task<Question> CreateQuestionAsync(QuestionDTO questionDto);

        Task<bool> UpdateQuestionAsync(int id, QuestionDTO questionDto);

    }
}
