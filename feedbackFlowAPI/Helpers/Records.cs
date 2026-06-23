using feedbackFlowAPI.DTOs;

namespace feedbackFlowAPI.Helpers
{
    public record StudentsScore(UserDTO User, QuestionQuestionSetDTO Question, int Score);
    public record SubjectScore(SubjectDTO Subject, ExamType ExamType, double Score);
    public record SubjectScoreCombined(SubjectDTO Subject, double Score);
    public record StudentScore(UserDTO User, double Score);
    public record StudentsAvgScoreSubject(UserDTO User, SubjectDTO Subject, double Score);
    public record WeakSubject(SubjectDTO Subject, double Score);
}
