using System;
using System.Collections.Generic;

namespace feedbackFlowAPI.Entities;

public class Subject
{
    public int Id { get; set; }

    /// <summary>
    /// trigonometri, vectors
    /// </summary>
    public string name { get; set; } = null!;

    public virtual ICollection<Question>? Questions { get; set; } = new List<Question>();

    public virtual ICollection<QuestionQuestionSet>? QuestionQuestionSet { get; set; } = new List<QuestionQuestionSet>();
}
