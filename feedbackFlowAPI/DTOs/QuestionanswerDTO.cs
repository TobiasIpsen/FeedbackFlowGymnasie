using System;
using System.Collections.Generic;

namespace feedbackFlowAPI.DTOs;

public partial class QuestionAnswerDTO
{
    public string? Name { get; set; }

    public string? Url { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }
}
