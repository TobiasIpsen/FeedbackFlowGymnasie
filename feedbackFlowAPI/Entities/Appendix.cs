using System;
using System.Collections.Generic;

namespace feedbackFlowAPI.Entities;

/// <summary>
/// (Billag)
/// </summary>
public class Appendix
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Url { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public int QuestionId { get; set; }

    public virtual Question Question { get; set; } = null!;
}
