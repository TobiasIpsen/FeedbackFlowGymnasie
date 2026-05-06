using feedbackFlowAPI.DTOs;
using feedbackFlowAPI.Entities;
using feedbackFlowAPI.Helpers;
using feedbackFlowAPI.Mappers.Implementations;
using feedbackFlowAPI.Mappers.Interface;
using feedbackFlowAPI.Services.Implementations;
using feedbackFlowAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace UnitTests
{
    public class QuestionSetTests
    {
        [Fact]
        public async Task QuestionsInQuestionSet_True()
        {
            /*
            Arrange:
            Create in memory DbContext
            Add entities to in memory db
            Create mapper
            Instantiate service
            Create test DTOS

            Act:
            Call CreateQuestionSets

            Assert:
            Questions are added
            Relationships are correct
            Data is saved
            */

            //Arrange:
            var options = new DbContextOptionsBuilder<FbfDbContext>()
                .UseInMemoryDatabase(databaseName: "db_" + Guid.NewGuid())
                .Options;

            QuestionSetDTO dto = new QuestionSetDTO
            {
                Name = "",
                IsExam = false,
                IsDraft = true,
                TeacherId = 1
            };

            await using (var context = new FbfDbContext(options))
            {
                IQuestionSetMapper mapper = new QuestionSetMapper();
                IQuestionSetService service = new QuestionSetService(context, mapper);

                await context.Users.AddAsync(new User { Firstname = "Tobias", Lastname = "I", Email = "a@a.dk" });
                await context.Courses.AddAsync(new Course { Name = "Math" });
                await context.Subjects.AddAsync(new Subject { Name = "Sub1" });

                Question q1 = new Question
                {
                    ImgSrc = "abc/123.png",
                    Points = "10",
                    ExamType = ExamType.Analog,
                    ClassLevel = ClassLevel.A,
                    QuestionDifficulty = QuestionDifficulty.Hard,
                    QuestionMethodRequirement = QuestionMethodRequirement.NoRequirement,
                    Education = Education.HF,
                    QuestionContext = QuestionContext.YesLight,
                    StandardQuestion = StandardQuestion.Yes,
                    NewOldSystem = NewOldSystem.New,
                    CourseId = 1,
                    UserId = 1,
                };
                Question q2 = new Question
                {
                    ImgSrc = "abc/456.png",
                    Points = "5",
                    ExamType = ExamType.Digital,
                    ClassLevel = ClassLevel.B,
                    QuestionDifficulty = QuestionDifficulty.Easy,
                    QuestionMethodRequirement = QuestionMethodRequirement.ApplyFormula,
                    Education = Education.HHX,
                    QuestionContext = QuestionContext.No,
                    StandardQuestion = StandardQuestion.WithATwist,
                    NewOldSystem = NewOldSystem.Old,
                    CourseId = 1,
                    UserId = 1,
                };
                await context.Questions.AddAsync(q1);
                await context.Questions.AddAsync(q2);

                await context.SaveChangesAsync();

                var course = await context.Courses.FirstAsync();
                var subject = await context.Subjects.FirstAsync();
                var user = await context.Users.FirstAsync();
                var questions = await context.Questions.ToListAsync();


                List<int> questionIds = questions.Select(q => q.Id).ToList();

                //Act:
                QuestionSetDTO answer = await service.CreateQuestionSet(questionIds, subject.Id, dto);


                //Assert:
                Assert.Equal(2, answer.Questions.Count);
                Assert.Contains(answer.Questions, q => q.QuestionId == q1.Id);
                Assert.Contains(answer.Questions, q => q.QuestionId == q2.Id);

                var dbAnswer = await context.QuestionSets.FindAsync(1);
                Assert.Equal(1, dbAnswer.Id);
                Assert.Equal(2, dbAnswer.Questions.Count);
            }
        }
    }
}
