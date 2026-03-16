using System;
using System.Collections.Generic;

namespace feedbackFlowAPI.DTOs;

public partial class QuestionSetDTO
{
    public string? Name { get; set; }

    public bool IsExam { get; set; }

    /// <summary>
    /// if its assigned to students
    /// </summary>
    public bool IsDraft { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }
}
