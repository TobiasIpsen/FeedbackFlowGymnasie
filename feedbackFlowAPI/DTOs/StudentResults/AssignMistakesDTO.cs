using System.ComponentModel.DataAnnotations;

namespace feedbackFlowAPI.DTOs.StudentResults;

public class AssignMistakesDTO
{
    [Range(1, int.MaxValue, ErrorMessage = "TeacherId is required.")]
    public int? TeacherId { get; set; }

    [Required]
    public List<int>? MistakeIds { get; set; }
}
