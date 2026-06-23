using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.Helpers;
using feedbackFlowAPI.Mappers.Implementations;
using feedbackFlowAPI.Mappers.Interface;
using feedbackFlowAPI.Services.Implementations;
using feedbackFlowAPI.Services.Interfaces;
using FluentAssertions;

namespace UnitTests
{
    public class QuestionSetTests : IClassFixture<PostgreSqlTestFixture>
    {
        private PostgreSqlTestFixture _fixture;

        public QuestionSetTests(PostgreSqlTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory, MemberData(nameof(GetQuestionSet))]
        public async Task CreatedQuestionSet_ShouldBe_QuestionSet(QuestionSetDTO questionSetData)
        {
            //Arrange
            FbfDbContext _context = new FbfDbContext(_fixture.DbContextOptions);
            IQuestionSetMapper _mapper = new QuestionSetMapper();
            IQuestionSetService _service = new QuestionSetService(_context, _mapper);
            var questions = _context.Questions.Take(2).Select(q => q.Id).ToList();
            var subject = _context.Subjects.First();

            //Act
            var questionSet = await _service.CreateQuestionSet(questions, subject.Id, questionSetData);

            //Assert
            questionSet.Name.Should().Be(questionSetData.Name);
            questionSet.IsExam.Should().Be(questionSetData.IsExam);
            questionSet.IsDraft.Should().Be(questionSetData.IsDraft);
            questionSet.TeacherId.Should().Be(questionSetData.TeacherId);
            questionSet.Questions.Select(q => q.QuestionId).Should().Contain(questions);
            questionSet.Questions.Should().HaveCount(questions.Count);
            questionSet.Questions.Select(q => q.Order).Should().BeInAscendingOrder();
        }

        public static IEnumerable<object[]> GetQuestionSet()
        {
            yield return new object[]
            {
                new QuestionSetDTO
                {
                    Name = "Awesome Question Set",
                    IsExam = true,
                    IsDraft = false,
                    TeacherId = 2
                }
            };

            yield return new object[]
            {
                new QuestionSetDTO
                {
                    Name = "Very Cool Question Set",
                    IsExam = false,
                    IsDraft = true,
                    TeacherId = 1
                }
            };
        }
    }
}