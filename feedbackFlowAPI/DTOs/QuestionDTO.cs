using feedbackFlowAPI.Helpers;
using System;
using System.Collections.Generic;

namespace feedbackFlowAPI.DTOs;

public partial class QuestionDTO
{
    public int Id { get; set; }

    public string ImgSrc { get; set; } = null!;

    public string Points { get; set; } = null!;

    public IFormFile? File { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }

    public ExamType ExamType { get; set; }

    public ClassLevel ClassLevel { get; set; }

    public QuestionDifficulty QuestionDifficulty { get; set; }

    public QuestionMethodRequirement QuestionMethodRequirement { get; set; }

    public Education Education { get; set; }

    public QuestionContext QuestionContext { get; set; }

    public StandardQuestion StandardQuestion { get; set; }

    public NewOldSystem NewOldSystem { get; set; }
}