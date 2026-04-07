using System.ComponentModel.DataAnnotations;

namespace feedbackFlowAPI.DTOs;

public class AddStudentToClassDTO
{
    [Required]
    [Range(1, int.MaxValue)]
    public int? StudentId { get; set; }
}
