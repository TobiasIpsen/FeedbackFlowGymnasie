using System.ComponentModel.DataAnnotations;

namespace feedbackFlowAPI.Helpers.ControllerHelpers
{
    public class GetQuestionRequest
    {
        [Range(1, int.MaxValue, ErrorMessage = "Value must be positive")]
        public int? LastId { get; set; } = int.MaxValue;
        [Range(1, int.MaxValue, ErrorMessage = "Value must be positive")]
        public int PageSize { get; set; } = 20;
    }
}
