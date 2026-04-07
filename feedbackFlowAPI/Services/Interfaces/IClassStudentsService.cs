using feedbackFlowAPI.DTOs;

namespace feedbackFlowAPI.Services.Interfaces
{
    public interface IClassStudentsService
    {
        Task<List<ClassStudentDTO>?> GetClassStudentsAsync(int classId);
        Task<List<StudentSearchDTO>?> SearchStudentsAsync(int classId, string query);
        Task<(bool Success, string? Error)> AddStudentToClassAsync(int classId, int studentId);
        Task<(bool Success, string? Error)> RemoveStudentFromClassAsync(int classId, int studentId);
    }
}
