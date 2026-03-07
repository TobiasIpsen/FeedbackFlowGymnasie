using System;
using System.Collections.Generic;

namespace feedbackFlowAPI.Entities;

/// <summary>
/// (diagram, tekst)
/// </summary>
public class ContentType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<QuestionAnswer> QuestionAnswers { get; set; } = new List<QuestionAnswer>();
}
