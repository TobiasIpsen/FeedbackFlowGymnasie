using System;
using System.Collections.Generic;

namespace feedbackFlowAPI.DTOs.StudentResults;

public class StudentResultDTO
{
    public int? TeacherPoint { get; set; }

    public string? TeacherFeedback { get; set; }

    public string? StudentSelfAssessmentPoints { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }

    public int TeacherId { get; set; }

    public int QuestionId { get; set; }

    public int QuestionSetId { get; set; }
}
