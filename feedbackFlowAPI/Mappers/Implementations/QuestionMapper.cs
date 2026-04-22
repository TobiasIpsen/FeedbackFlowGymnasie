using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.Entities;
using feedbackFlowAPI.Helpers.ControllerHelpers;
using feedbackFlowAPI.Mappers.Interface;

namespace feedbackFlowAPI.Mappers.Implementations
{
    public class QuestionMapper : IQuestionMapper
    {
        public QuestionDTO ToDTO(Question entity)
        {
            return new QuestionDTO
            {
                Id = entity.Id,
                ImgSrc = entity.ImgSrc,
                Points = entity.Points,
                ExamType = entity.ExamType,
                ClassLevel = entity.ClassLevel,
                QuestionDifficulty = entity.QuestionDifficulty,
                QuestionMethodRequirement = entity.QuestionMethodRequirement,
                Education = entity.Education,
                QuestionContext = entity.QuestionContext,
                StandardQuestion = entity.StandardQuestion,
                NewOldSystem = entity.NewOldSystem                
            };
        }
    }
}
