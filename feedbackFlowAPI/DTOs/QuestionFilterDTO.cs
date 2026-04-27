using feedbackFlowAPI.Helpers;

namespace feedbackFlowAPI.DTOs;

public class QuestionFilterDTO
{
    public ClassLevel? ClassLevel { get; set; }

    public Education? Education { get; set; }

    public ExamType? ExamType { get; set; }

    public QuestionDifficulty? QuestionDifficulty { get; set; }

    public int? SubjectId { get; set; }

    public string? Subject { get; set; }

    public int? Year { get; set; }

    public string? ClassName { get; set; }

    public string? FreeText { get; set; }
}
