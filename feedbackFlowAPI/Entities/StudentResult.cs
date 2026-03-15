using System;
using System.Collections.Generic;

namespace feedbackFlowAPI.Entities;

public class StudentResult
{
    public int Id { get; set; }

    public string? TeacherPoint { get; set; }

    public string? TeacherFeedback { get; set; }

    public string? StudentSelfAssessmentPoints { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }

    public int TeacherId { get; set; }

    public int StudentId { get; set; }

    public int QuestionId { get; set; }

    public int QuestionSetId { get; set; }

    public Question Question { get; set; } = null!;

    public QuestionSet QuestionSet { get; set; } = null!;

    public User Teacher { get; set; } = null!;

    public User Student { get; set; } = null!;

    public ICollection<Mistake> Mistakes { get; set; } = new List<Mistake>();
}
