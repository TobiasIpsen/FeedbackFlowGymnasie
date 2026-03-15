using feedbackFlowAPI.Helpers;
using System;
using System.Collections.Generic;

namespace feedbackFlowAPI.DTOs;

public partial class QuestionDTO
{
    public string ImgSrc { get; set; } = null!;

    public string Points { get; set; } = null!;

    public DateTimeOffset IsDeleted { get; set; }

    public ExamType ExamType { get; set; }

    public ClassLevel ClassLevel { get; set; }

    public QuestionDifficulty QuestionDifficulity { get; set; }

    public QuestionMethodRequirement QuestionMethodRequirement { get; set; }

    public Education Eudcation { get; set; }

    public StandardQuestion StandardQuestion { get; set; }
}