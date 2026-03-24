using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.Entities;

namespace feedbackFlowAPI.Mappers
{
    public static class StudentResultMapper
    {
        public static StudentResult ToEntity(
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

        public static TeacherFeedbackDTO ToDTO(StudentResult entity)
        {
            return new TeacherFeedbackDTO
            {
                TeacherPoint = entity.TeacherPoint,
                TeacherFeedback = entity.TeacherFeedback,
                TeacherId = entity.TeacherId
            };
        }
    }
}
