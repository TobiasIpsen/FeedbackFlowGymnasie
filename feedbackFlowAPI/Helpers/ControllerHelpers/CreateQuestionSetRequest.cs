using feedbackFlowAPI.DTOs;

namespace feedbackFlowAPI.Helpers.ControllerHelpers
{
    public class CreateQuestionSetRequest
    {
        public List<int> QuestionIds { get; set; } = new List<int>();
        public QuestionSetDTO Set { get; set; }
        public int SubjectId { get; set; }
    }
}
