using System.ComponentModel.DataAnnotations;

namespace feedbackFlowAPI.DTOs.StudentResults
{
    public class StudentSelfAssessmentDTO
    {
        [Range(0, 10, ErrorMessage = "Points must be between 0 and 10")]
        public int points { get; set; }
    }
}
