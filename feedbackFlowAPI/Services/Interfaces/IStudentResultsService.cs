using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.DTOs.StudentResults;
using Microsoft.AspNetCore.Mvc;

namespace feedbackFlowAPI.Services.Interfaces
{
    public interface IStudentResultsService
    {
        public Task<TeacherFeedbackDTO> SaveTeacherFeedbackAsync(int studentId, int questionSetId, int questionId, TeacherFeedbackDTO dto);
        public Task<List<MistakeTypeDTO>> GetMistakeTypesAsync();
        public Task<List<MistakeTypeDTO>?> GetAssignedMistakesAsync(int studentId, int questionSetId, int questionId, int teacherId);
        public Task<(bool Success, string? Error)> AssignMistakesAsync(int studentId, int questionSetId, int questionId, AssignMistakesDTO dto);
        public Task<List<StudentDashboardAssignmentDTO>> GetStudentDashboardAssignmentsAsync(int studentId);
    }
}
