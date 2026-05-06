namespace feedbackFlowFront.Service
{
    public class AssignmentService
    {
        public Task<List<StudentAssignmentItem>> GetStudentAssignmentsAsync(int studentId)
        {
            var items = new List<StudentAssignmentItem>
            {
                new("Skriftlig analyse - Dansk", DateTime.Today.AddDays(2), "I gang"),
                new("Fysik rapport - Kraft", DateTime.Today.AddDays(5), "Planlagt"),
                new("Historie essay - Kold krig", DateTime.Today.AddDays(7), "Planlagt")
            };

            return Task.FromResult(items);
        }

        public Task<int> GetActiveClassesForTeacherAsync(int teacherId)
        {
            return Task.FromResult(4);
        }

        public record StudentAssignmentItem(string Title, DateTime DueDate, string Status);
    }
}
