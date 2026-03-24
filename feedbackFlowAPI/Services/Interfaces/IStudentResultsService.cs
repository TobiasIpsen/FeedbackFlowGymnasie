using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.DTOs.StudentResults;
using Microsoft.AspNetCore.Mvc;

namespace feedbackFlowAPI.Services.Interfaces
{
    public interface IStudentResultsService
    {
        public Task<TeacherFeedbackDTO> SaveTeacherFeedbackAsync(int studentId, int questionSetId, int questionId, TeacherFeedbackDTO dto);
    }
}
