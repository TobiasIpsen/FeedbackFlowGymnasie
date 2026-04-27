using feedbackFlowAPI.DTOs;

namespace feedbackFlowAPI.Services.Interfaces
{
    public interface IClassService
    {
        public Task<ClassDTO> CreateClass(ClassDTO dto);
        public Task<List<ClassStudentDTO>> GetStudentsForClass(int classId);
        public Task<List<StudentLookupDTO>> SearchStudents(string? query, int? classId);
        public Task<bool> AddStudentToClass(int classId, int studentId);
        public Task<bool> RemoveStudentFromClass(int classId, int studentId);
    }
}
