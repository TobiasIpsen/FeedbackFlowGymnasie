using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.DTOs.StudentResults;
using feedbackFlowAPI.Entities;
using feedbackFlowAPI.Mappers.Interface;

namespace feedbackFlowAPI.Mappers.Implementations
{
    public class StudentResultMapper : IStudentResultMapper
    {
        public StudentResult ToEntity(
            TeacherFeedbackDTO dto,
            int studentId,
            int questionSetId,
            int questionId)
        {
            return new StudentResult
            {
                StudentId = studentId,
                QuestionSetId = questionSetId,
                QuestionId = questionId,
                TeacherPoint = dto.TeacherPoint,
                TeacherFeedback = dto.TeacherFeedback,
                TeacherId = dto.TeacherId
            };
        }

        public TeacherFeedbackDTO ToTeacherFeedbackDTO(StudentResult entity)
        {
            return new TeacherFeedbackDTO
            {
                TeacherPoint = entity.TeacherPoint,
                TeacherFeedback = entity.TeacherFeedback,
                TeacherId = entity.TeacherId
            };
        }

        public StudentResultDTO ToStudentResultDTO(StudentResult entity)
        {
            return new StudentResultDTO
            {
                TeacherPoint = entity.TeacherPoint,
                TeacherFeedback = entity.TeacherFeedback,
                StudentSelfAssessmentPoints = entity.StudentSelfAssessmentPoints,
                CreatedAt = entity.CreatedAt,
                DeletedAt = entity.DeletedAt,
                TeacherId = entity.TeacherId,
                QuestionId = entity.QuestionId,
                QuestionSetId = entity.QuestionSetId
            };
        }
    }
}
