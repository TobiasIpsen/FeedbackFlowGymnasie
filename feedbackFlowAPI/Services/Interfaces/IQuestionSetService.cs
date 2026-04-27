using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.Helpers.ControllerHelpers;

namespace feedbackFlowAPI.Services.Interfaces
{
    public interface IQuestionSetService
    {
        public Task<QuestionSetDTO> CreateQuestionSet(List<int> QuestionÌds, int SubjectId, QuestionSetDTO Set);
        public Task<List<ClassListItemDTO>> GetAssignableClassesAsync();
        public Task<QuestionSetAssignmentResultDTO> AssignQuestionSetToClassAsync(int questionSetId, int classId);
    }
}
