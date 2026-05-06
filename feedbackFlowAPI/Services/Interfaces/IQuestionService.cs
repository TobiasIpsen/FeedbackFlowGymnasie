using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.Helpers.ControllerHelpers;

namespace feedbackFlowAPI.Services.Interfaces
{
    public interface IQuestionService
    {
        Task<GetQuestionResponse> GetQuestions(GetQuestionRequest getQuestionRequest);
    }
}
