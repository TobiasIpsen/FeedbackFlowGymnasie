using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.DTOs.ClassStatistics;
using feedbackFlowAPI.DTOs.StudentLookupDTO;

namespace feedbackFlowAPI.Services.Interfaces
{
    public interface IClassService
    {
        public Task<ClassDTO> CreateClass(ClassDTO dto);
        public Task<List<ClassStudentDTO>> GetStudentsForClass(int classId);
        public Task<List<StudentLookupDTO>> SearchStudents(string? query, int? classId);
        public Task<bool> AddStudentToClass(int classId, int studentId);
        public Task<ClassStatisticsDTO> GetStatistics(int ClassId, int QuestionSet);
    }
}
