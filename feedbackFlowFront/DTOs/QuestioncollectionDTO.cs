using System;
using System.Collections.Generic;

namespace feedbackFlowAPI.DTOs;

public partial class QuestioncollectionDTO
{
    public int Id { get; set; }

    public string? Points { get; set; }

    public int? Sequence { get; set; }
}
