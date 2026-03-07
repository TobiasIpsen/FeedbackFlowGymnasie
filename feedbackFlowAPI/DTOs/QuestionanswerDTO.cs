using System;
using System.Collections.Generic;

namespace feedbackFlowAPI.DTOs;

public partial class QuestionanswerDTO
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Url { get; set; }

    public bool Isdeleted { get; set; }
}
