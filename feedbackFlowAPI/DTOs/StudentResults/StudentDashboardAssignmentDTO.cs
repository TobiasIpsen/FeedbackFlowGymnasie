namespace feedbackFlowAPI.DTOs.StudentResults;

public class StudentDashboardAssignmentDTO
{
    public int QuestionSetId { get; set; }

    public string? QuestionSetName { get; set; }

    public bool IsExam { get; set; }

    public DateTimeOffset AssignedAt { get; set; }

    public int QuestionCount { get; set; }

    public string TeacherName { get; set; } = string.Empty;
}
