using System;
using System.Collections.Generic;

namespace feedbackFlowAPI.Entities;

public class StudentResult
{
    public int Id { get; set; }

    public string? TeacherPoint { get; set; }

    public string? TeacherFeedback { get; set; }

    public string? StudentSelfAssessmentPoints { get; set; }

    public DateTime CreatedDate { get; set; }

    public bool IsDeleted { get; set; }

    public int? TeacherId { get; set; }

    public int? StudentId { get; set; }

    public int QuestionId { get; set; }

    public int QuestionSetId { get; set; }

    public virtual Question Question { get; set; } = null!;

    public virtual QuestionSet QuestionSet { get; set; } = null!;

    public virtual User Teacher { get; set; } = null!;

    public virtual User Student { get; set; } = null!;

    public virtual ICollection<ErrorType>? ErrorTypes { get; set; } = new List<ErrorType>();
}
