namespace feedbackFlowAPI.DTOs;

public class ClassListItemDTO
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int Year { get; set; }

    public int StudentCount { get; set; }
}

public class QuestionSetAssignmentResultDTO
{
    public int QuestionSetId { get; set; }

    public int ClassId { get; set; }

    public int AssignedStudentCount { get; set; }

    public int AssignedQuestionCount { get; set; }

    public List<StudentNotificationDTO> Notifications { get; set; } = new();
}

public class StudentNotificationDTO
{
    public int StudentId { get; set; }

    public string StudentEmail { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;
}
