using System;
using System.Collections.Generic;

namespace feedbackFlowAPI.DTOs;

public partial class QuestionDTO
{
    public int Id { get; set; }

    public string Points { get; set; } = null!;

    public bool Isdeleted { get; set; }
}