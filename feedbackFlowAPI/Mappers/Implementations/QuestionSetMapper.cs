using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.Entities;
using feedbackFlowAPI.Helpers.ControllerHelpers;
using feedbackFlowAPI.Mappers.Interface;

namespace feedbackFlowAPI.Mappers.Implementations
{
    public class QuestionSetMapper : IQuestionSetMapper
    {
        public QuestionSetDTO ToDTO(QuestionSet entity)
        {
            return new QuestionSetDTO
            {
                id = entity.Id,
                Name = entity.Name,
                IsExam = entity.IsExam,
                IsDraft = entity.IsDraft,
                DeletedAt = entity.DeletedAt,
                TeacherId = entity.TeacherId,
                Questions = entity.Questions
                    .OrderBy(q => q.Order)
                    .Select(q => 
                        new QuestionQuestionSetDTO
                        {
                            SubjectId = q.SubjectId,
                            QuestionId = q.QuestionId,
                            Order = q.Order,
                            QuestionSetId = q.QuestionSetId
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
                    .Select((id, index) =>
                        new QuestionQuestionSet
                        {
                            SubjectId = subjectId,
                            QuestionId = id,
                            Order = index
                        })
                    .ToList()
            };
        }
    }
}
