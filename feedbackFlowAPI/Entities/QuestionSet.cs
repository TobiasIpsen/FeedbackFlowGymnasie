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

    public DateTimeOffset IsDeleted { get; set; }

    public int TeacherId { get; set; }

    public User Teacher { get; set; } = null!;

    public ICollection<QuestionAnswer>? QuestionAnswers { get; set; } = new List<QuestionAnswer>();

    public ICollection<StudentResult>? StudentResults { get; set; } = new List<StudentResult>();

    public ICollection<QuestionQuestionSet> Question { get; set; } = new List<QuestionQuestionSet>();
}
