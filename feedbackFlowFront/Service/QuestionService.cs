using feedbackFlowAPI.DTOs;

namespace feedbackFlowFront.Service
{
    public class QuestionService
    {
        private readonly List<QuestionDTO> _questions = new();

        public Task CreateQuestionAsync(QuestionDTO question)
        {
            _questions.Add(new QuestionDTO
            {
                ImgSrc = question.ImgSrc,
                Points = question.Points,
                DeletedAt = question.DeletedAt,
                ExamType = question.ExamType,
                ClassLevel = question.ClassLevel,
                QuestionDifficulty = question.QuestionDifficulty,
                QuestionMethodRequirement = question.QuestionMethodRequirement,
                Education = question.Education,
                StandardQuestion = question.StandardQuestion
            });

            return Task.CompletedTask;
        }

        public Task<IReadOnlyList<QuestionDTO>> GetQuestionsAsync()
        {
            return Task.FromResult((IReadOnlyList<QuestionDTO>)_questions);
        }
    }
}
