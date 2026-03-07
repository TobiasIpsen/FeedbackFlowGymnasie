using System.Text.Json.Serialization;

namespace feedbackFlowAPI.Helpers
{
    public enum ClassLevel
    {
        A,
        B,
        C,
        D,
        E,
        F
    }

    public enum QuestionType
    {
        Digital,
        Analog
    }

    public enum UserRole
    {
        Student,
        Teacher,
        Admin
    }

    public enum Visibility
    {
        Private,
        Public
    }
}
