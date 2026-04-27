namespace feedbackFlowFront.Service
{
    public class SelfAssessmentService
    {
        public Task<int> GetPendingSelfAssessmentsAsync(int studentId)
        {
            return Task.FromResult(2);
        }


    }
}
