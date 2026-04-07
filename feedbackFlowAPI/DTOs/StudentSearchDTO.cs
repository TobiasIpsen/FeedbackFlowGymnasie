namespace feedbackFlowAPI.DTOs;

public class StudentSearchDTO
{
    public int Id { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool IsInClass { get; set; }
}
