using System;
using System.Collections.Generic;

namespace feedbackFlowAPI.Entities;

public class QuestionSet
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool IsExam { get; set; }

    /// <summary>
    /// if its assigned to students
    /// </summary>
    public bool IsDraft { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<QuestionAnswer>? QuestionAnswers { get; set; } = new List<QuestionAnswer>();

    public virtual ICollection<StudentResult>? StudentResults { get; set; } = new List<StudentResult>();

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
}
