namespace feedbackFlowFront.Service
{
    public class GradingService
    {
        public Task<List<TeacherQueueItem>> GetPendingReviewsAsync(int teacherId)
        {
            var queue = new List<TeacherQueueItem>
            {
                new("2.g - Matematik", "Funktioner aflevering", 8),
                new("1.g - Dansk", "Novelleanalyse", 6),
                new("3.g - Historie", "Kildekritik", 4)
            };

            return Task.FromResult(queue);
        }

        public Task<LatestFeedbackItem> GetLatestFeedbackForStudentAsync(int studentId)
        {
            var latest = new LatestFeedbackItem(
                "Matematik opgave 5",
                DateTime.Today.AddDays(-1),
                "Laest");

            return Task.FromResult(latest);
        }

        public record TeacherQueueItem(string ClassName, string AssignmentTitle, int PendingCount);
        public record LatestFeedbackItem(string AssignmentTitle, DateTime PublishedAt, string Status);

    }
}
