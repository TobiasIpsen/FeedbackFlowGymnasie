using feedbackFlowAPI.Helpers;
using System;
using System.Collections.Generic;

namespace feedbackFlowAPI.Entities;

public class QuestionAnswer
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Url { get; set; }

    public Visibility Visibility { get; set; }

    public bool IsDeleted { get; set; }

    public int QuestionId { get; set; }

    /// <summary>
    /// f.feks. svar til hele questionset i stedet for kun 1 question
    /// </summary>
    public int QuestionSetId { get; set; }

    public virtual Question Question { get; set; } = null!;

    public virtual QuestionSet? QuestionSet { get; set; } = null!;

    public virtual ICollection<ContentType> ContentTypes { get; set; } = new List<ContentType>();
}
