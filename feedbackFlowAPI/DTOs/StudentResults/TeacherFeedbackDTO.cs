using System.ComponentModel.DataAnnotations;

namespace feedbackFlowAPI.DTOs;

public class TeacherFeedbackDTO
{
    [Range(0, 10, ErrorMessage = "Points must be between 0 and 10.")]
    public int? TeacherPoint { get; set; }

    public string? TeacherFeedback { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "TeacherId is required.")]
    public int TeacherId { get; set; }

    public TeacherFeedbackDTO() {}
}
