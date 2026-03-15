using feedbackFlowAPI.Helpers;
using System;
using System.Collections.Generic;

namespace feedbackFlowAPI.DTOs;

public partial class QuestionDTO
{
    public string ImgSrc { get; set; } = null!;

    public string Points { get; set; } = null!;

    public DateTimeOffset? DeletedAt { get; set; }

    public ExamType ExamType { get; set; }

    public ClassLevel ClassLevel { get; set; }

    public QuestionDifficulty QuestionDifficulty { get; set; }

    public QuestionMethodRequirement QuestionMethodRequirement { get; set; }

    public Education Education { get; set; }

    public StandardQuestion StandardQuestion { get; set; }
}