namespace feedbackFlowAPI.Entities
{
    public class QuestionQuestionSet
    {
        public int SubjectId { get; set; }

        public int QuestionId { get; set; }
        
        public int QuestionSetId { get; set; }

        public Subject Subject { get; set; } = null!;

        public virtual Question Question { get; set; } = null!;
        
        public virtual QuestionSet QuestionSet { get; set; } = null!;
    }
}
