namespace feedbackFlowAPI.Entities
{
    public class Mistake
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public ICollection<StudentResult> StudentResults { get; set; } = new List<StudentResult>();
    }
}
