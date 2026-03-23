using System;
using System.ComponentModel.DataAnnotations;
using feedbackFlowAPI.Helpers;

namespace feedbackFlowAPI.DTOs
{
    public class QuestionDto
    {
        [Required]
        public string ImgSrc { get; set; } = null!;

        [Required]
        public string Points { get; set; } = null!;

        public DateTimeOffset? DeletedAt { get; set; }

        [Required]
        public ExamType ExamType { get; set; }

        [Required]
        public ClassLevel ClassLevel { get; set; }

        [Required]
        public QuestionDifficulty QuestionDifficulty { get; set; }

        [Required]
        public QuestionMethodRequirement QuestionMethodRequirement { get; set; }

        [Required]
        public Education Education { get; set; }

        [Required]
        public QuestionContext QuestionContext { get; set; }

        [Required]
        public StandardQuestion StandardQuestion { get; set; }

        [Required]
        public NewOldSystem NewOldSystem { get; set; }

        public int? CourseId { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}