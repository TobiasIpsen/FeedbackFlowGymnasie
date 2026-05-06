using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.Entities;
using feedbackFlowAPI.Helpers.ControllerHelpers;

namespace feedbackFlowAPI.Mappers.Interface
{
    public interface IQuestionSetMapper
    {
        public QuestionSet ToEntity(List<int> questionIds, int subjectId, QuestionSetDTO dto);
        public QuestionSetDTO ToDTO(QuestionSet entity);
    }
}
