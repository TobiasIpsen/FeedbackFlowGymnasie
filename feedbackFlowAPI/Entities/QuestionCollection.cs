using System;
using System.Collections.Generic;

namespace feedbackFlowAPI.Entities;

public class QuestionCollection
{
    public int Id { get; set; }

    public string? Points { get; set; }

    public int? Sequence { get; set; }

    public int QuestionId { get; set; }

    public virtual Question Question { get; set; } = null!;
}
