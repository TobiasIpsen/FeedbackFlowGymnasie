using feedbackFlowAPI.DTOs;

namespace feedbackFlowFront.Service
{
    public class QuestionService
    {
        private readonly List<QuestionListItem> _questions = new();
        private int _nextId = 1;

        public Task<QuestionListItem> CreateQuestionAsync(QuestionDTO question)
        {
            var created = new QuestionListItem
            {
                Id = _nextId++,
                ImgSrc = question.ImgSrc,
                Points = question.Points,
                DeletedAt = question.DeletedAt,
                ExamType = question.ExamType,
                ClassLevel = question.ClassLevel,
                QuestionDifficulty = question.QuestionDifficulty,
                QuestionMethodRequirement = question.QuestionMethodRequirement,
                Education = question.Education,
                StandardQuestion = question.StandardQuestion
            };

            _questions.Add(created);

            return Task.FromResult(Clone(created));
        }

        public Task<IReadOnlyList<QuestionListItem>> GetQuestionsAsync()
        {
            IReadOnlyList<QuestionListItem> copy = _questions
                .Select(Clone)
                .ToList();

            return Task.FromResult(copy);
        }

        private static QuestionListItem Clone(QuestionListItem question)
        {
            return new QuestionListItem
            {
                Id = question.Id,
                ImgSrc = question.ImgSrc,
                Points = question.Points,
                DeletedAt = question.DeletedAt,
                ExamType = question.ExamType,
                ClassLevel = question.ClassLevel,
                QuestionDifficulty = question.QuestionDifficulty,
                QuestionMethodRequirement = question.QuestionMethodRequirement,
                Education = question.Education,
                StandardQuestion = question.StandardQuestion
            };
        }
    }

    public class QuestionListItem : QuestionDTO
    {
        public int Id { get; set; }
    }

    public class QuestionSetService
    {
        private readonly List<QuestionSetItem> _questionSets = new();
        private int _nextId = 1;

        public Task<QuestionSetItem> CreateQuestionSetAsync(string? name, bool isExam, bool isDraft, IReadOnlyCollection<int> questionIds)
        {
            var created = new QuestionSetItem
            {
                Id = _nextId++,
                Name = name,
                IsExam = isExam,
                IsDraft = isDraft,
                QuestionIds = questionIds
                    .Distinct()
                    .ToList(),
                CreatedAt = DateTimeOffset.UtcNow
            };

            _questionSets.Add(created);
            return Task.FromResult(Clone(created));
        }

        public Task<IReadOnlyList<QuestionSetItem>> GetQuestionSetsAsync()
        {
            IReadOnlyList<QuestionSetItem> copy = _questionSets
                .Select(Clone)
                .ToList();

            return Task.FromResult(copy);
        }

        private static QuestionSetItem Clone(QuestionSetItem questionSet)
        {
            return new QuestionSetItem
            {
                Id = questionSet.Id,
                Name = questionSet.Name,
                IsExam = questionSet.IsExam,
                IsDraft = questionSet.IsDraft,
                CreatedAt = questionSet.CreatedAt,
                QuestionIds = questionSet.QuestionIds.ToList()
            };
        }
    }

    public class QuestionSetItem
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsExam { get; set; }
        public bool IsDraft { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public List<int> QuestionIds { get; set; } = new();
    }
}
