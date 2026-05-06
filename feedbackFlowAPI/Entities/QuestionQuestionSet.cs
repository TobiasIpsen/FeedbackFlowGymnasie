namespace feedbackFlowAPI.Entities
{
    public class QuestionQuestionSet
    {
        public int SubjectId { get; set; }

        public int QuestionId { get; set; }
        
        public int QuestionSetId { get; set; }

        public Subject Subject { get; set; } = null!;

        public Question Question { get; set; } = null!;
        
        public QuestionSet QuestionSet { get; set; } = null!;

        public int Order { get; set; }
    }
}
