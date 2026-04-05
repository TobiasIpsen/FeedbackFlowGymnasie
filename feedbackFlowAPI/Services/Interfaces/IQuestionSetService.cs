using feedbackFlowAPI.DTOs;

namespace feedbackFlowAPI.Services.Interfaces
{
    public interface IQuestionSetService
    {
        public Task<QuestionSetDTO> CreateQuestionSet(List<int> Questions, int SubjectId, QuestionSetDTO Set);
    }
}
