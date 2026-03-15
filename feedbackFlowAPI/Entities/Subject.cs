using System;
using System.Collections.Generic;

namespace feedbackFlowAPI.Entities;

public class Subject
{
    public int Id { get; set; }

    /// <summary>
    /// Emne.
    /// trigonometri, vectors
    /// </summary>
    public string Name { get; set; } = null!;

    public ICollection<Question>? Questions { get; set; } = new List<Question>();

    public ICollection<QuestionQuestionSet>? QuestionQuestionSet { get; set; } = new List<QuestionQuestionSet>();
}
