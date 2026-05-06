namespace feedbackFlowFront.Service
{
    public class UserService
    {
        public Task<string> GetStudentNameAsync(int studentId)
        {
            return Task.FromResult("Maja Jensen");
        }

        public Task<string> GetTeacherNameAsync(int teacherId)
        {
            return Task.FromResult("Anders Holm");
        }

    }
}
