using System;
using System.Collections.Generic;

namespace feedbackFlowAPI.DTOs;

public partial class StudentResultDTO
{
    public string? TeacherPoint { get; set; }

    public string? TeacherFeedback { get; set; }

    public string? StudentSelfAssessmentPoints { get; set; }

    public DateTimeOffset CreatedDate { get; set; }

    public DateTimeOffset Isdeleted { get; set; }
}
