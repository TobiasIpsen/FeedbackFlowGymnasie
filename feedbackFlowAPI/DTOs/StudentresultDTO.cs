using System;
using System.Collections.Generic;

namespace feedbackFlowAPI.DTOs;

public partial class StudentresultDTO
{
    public int Id { get; set; }

    public string? Teacherpoint { get; set; }

    public string? Teacherfeedback { get; set; }

    public string? Studentselfassessmentpoints { get; set; }

    public DateTime Createddate { get; set; }

    public bool Isdeleted { get; set; }
}
