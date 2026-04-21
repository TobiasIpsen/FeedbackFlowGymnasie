using System;
using System.Collections.Generic;

namespace feedbackFlowAPI.DTOs;

public partial class QuestionSetDTO
{
    public int id { get; set; }

    public string? Name { get; set; }

    public bool IsExam { get; set; }

    /// <summary>
    /// if its assigned to students
    /// </summary>
    public bool IsDraft { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }

    public int TeacherId { get; set; }

    public ICollection<QuestionQuestionSetDTO> Questions { get; set; } = new List<QuestionQuestionSetDTO>();
}
