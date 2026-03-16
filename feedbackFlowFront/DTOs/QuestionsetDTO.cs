using System;
using System.Collections.Generic;

namespace feedbackFlowAPI.DTOs;

public partial class QuestionsetDTO
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool Isexam { get; set; }

    /// <summary>
    /// if its assigned to students
    /// </summary>
    public bool Isdraft { get; set; }

    public bool Isdeleted { get; set; }
}
