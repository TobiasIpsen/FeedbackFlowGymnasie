using feedbackFlowAPI.Helpers;

namespace feedbackFlowAPI.DTOs;

public class QuestionUpsertDTO
{
    public string ImgSrc { get; set; } = null!;

    public string Points { get; set; } = null!;

    public DateTimeOffset? DeletedAt { get; set; }

    public ExamType ExamType { get; set; }

    public ClassLevel ClassLevel { get; set; }

    public QuestionDifficulty QuestionDifficulty { get; set; }

    public QuestionMethodRequirement QuestionMethodRequirement { get; set; }

    public Education Education { get; set; }

    public QuestionContext QuestionContext { get; set; }

    public StandardQuestion StandardQuestion { get; set; }

    public NewOldSystem NewOldSystem { get; set; }

    public int? CourseId { get; set; }

    public int UserId { get; set; }
}
