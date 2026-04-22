using feedbackFlowAPI.DTOs;

namespace feedbackFlowAPI.Helpers.ControllerHelpers
{
    public class GetQuestionResponse
    {
        public List<QuestionDTO> Questions { get; set; } = new List<QuestionDTO>();
        public int? LastId { get; set; }
    }
}
