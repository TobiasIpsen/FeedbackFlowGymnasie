using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.Entities;

namespace feedbackFlowAPI.Mappers.Interface
{
    public interface IQuestionSetMapper
    {
        public QuestionSet ToEntity(List<int> QuestionIds, int subjectId, QuestionSetDTO dto);
        public QuestionSetDTO ToDTO(QuestionSet entity);
    }
}
