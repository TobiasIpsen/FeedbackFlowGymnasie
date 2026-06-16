using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.DTOs.StudentResults;
using feedbackFlowAPI.Entities;

namespace feedbackFlowAPI.Mappers.Interface
{
    public interface IStudentResultMapper
    {
        public StudentResult ToEntity(TeacherFeedbackDTO dto, int studentId, int questionSetId, int questionId);
        public TeacherFeedbackDTO ToDTO(StudentResult entity);
        public StudentResultDTO StudentResultToDTO(StudentResult entity);
    }
}
