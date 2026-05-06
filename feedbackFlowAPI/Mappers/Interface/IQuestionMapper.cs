using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.Entities;
using feedbackFlowAPI.Helpers.ControllerHelpers;

namespace feedbackFlowAPI.Mappers.Interface
{
    public interface IQuestionMapper
    {
        public QuestionDTO ToDTO(Question entity);
    }
}
