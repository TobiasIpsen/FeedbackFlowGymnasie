using System;
using System.Collections.Generic;
using feedbackFlowAPI.Helpers;

namespace feedbackFlowAPI.DTOs;

public partial class QuestionDTO
{
    public string ImgSrc { get; set; } = string.Empty;

    public string Points { get; set; } = string.Empty;

    public DateTimeOffset? DeletedAt { get; set; }

    public ExamType ExamType { get; set; }

    public ClassLevel ClassLevel { get; set; }

    public QuestionDifficulty QuestionDifficulty { get; set; }

    public QuestionMethodRequirement QuestionMethodRequirement { get; set; }

    public Education Education { get; set; }

    public StandardQuestion StandardQuestion { get; set; }
}