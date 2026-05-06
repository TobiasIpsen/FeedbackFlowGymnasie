using feedbackFlowAPI.DTOs;

namespace feedbackFlowAPI.Helpers.ControllerHelpers
{
    public class CreateQuestionSetRequest
    {
        public required List<int> QuestionIds { get; set; } = new List<int>();
        public required QuestionSetDTO Set { get; set; }
        public required int SubjectId { get; set; }
    }
}
