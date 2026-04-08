using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.Entities;
using feedbackFlowAPI.Mappers.Interface;

namespace feedbackFlowAPI.Mappers.Implementations
{
    public class QuestionSetMapper : IQuestionSetMapper
    {
        public QuestionSetDTO ToDTO(QuestionSet entity)
        {
            return new QuestionSetDTO
            {
                Name = entity.Name,
                IsExam = entity.IsExam,
                IsDraft = entity.IsDraft,
                DeletedAt = entity.DeletedAt,
                TeacherId = entity.TeacherId,
                Questions = entity.Questions
                    .Select(q => 
                        new QuestionQuestionSetDTO
                        {
                            QuestionId = q.QuestionId,
                        })
                    .ToList(),
            };
        }

        public QuestionSet ToEntity(List<int> questionIds, int subjectId, QuestionSetDTO dto)
        {
            return new QuestionSet
            {
                Name = dto.Name,
                IsExam = dto.IsExam,
                IsDraft = dto.IsDraft,
                DeletedAt = dto.DeletedAt,
                TeacherId = dto.TeacherId,
                Questions = questionIds
                    .Select(id =>
                        new QuestionQuestionSet
                        {
                            SubjectId = subjectId,
                            QuestionId = id,
                        })
                    .ToList()
            };
        }
    }
}
