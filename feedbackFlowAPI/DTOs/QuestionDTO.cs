using System;
using System.ComponentModel.DataAnnotations;
using feedbackFlowAPI.Helpers;

namespace feedbackFlowAPI.DTOs;
    public class QuestionDTO
    { 
        
        public int Id { get; set; }

    [Required]
    public string ImgSrc { get; set; } = null!;

    [Required]
    public string Points { get; set; } = null!;

    public IFormFile? File { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }

    [Required]
    public ExamType? ExamType { get; set; }

    [Required]
    public ClassLevel? ClassLevel { get; set; }

    [Required]
    public QuestionDifficulty? QuestionDifficulty { get; set; }

    [Required]
    public QuestionMethodRequirement? QuestionMethodRequirement { get; set; }

    [Required]
    public Education? Education { get; set; }

    [Required]
    public QuestionContext? QuestionContext { get; set; }

    [Required]
    public StandardQuestion? StandardQuestion { get; set; }

    [Required]
    public NewOldSystem? NewOldSystem { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "CourseId must be positive")]
    public int? CourseId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "UserId is required and must be positive")]
    public int? UserId { get; set; }
}