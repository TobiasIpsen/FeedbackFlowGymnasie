namespace feedbackFlowAPI.DTOs;

public class ClassStudentDTO
{
    public int Id { get; set; }

    public string Firstname { get; set; } = string.Empty;

    public string Lastname { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
}

public class StudentLookupDTO
{
    public int Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public bool IsInClass { get; set; }
}

public class ClassStudentMutationDTO
{
    public int StudentId { get; set; }
}
