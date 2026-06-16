using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.Helpers.ControllerHelpers;

namespace feedbackFlowAPI.Services.Interfaces
{
    public interface IQuestionSetService
    {
        //test
        public Task<QuestionSetDTO> CreateQuestionSet(List<int> QuestionÌds, int SubjectId, QuestionSetDTO Set);
        public Task<byte[]> CreateQuestionSetAndGeneratePdf(List<int> QuestionIds, int SubjectId, QuestionSetDTO Set);
    }
}
