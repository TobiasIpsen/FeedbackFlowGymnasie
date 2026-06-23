

using feedbackFlowAPI.DTOs.ClassStatistics;
using feedbackFlowAPI.Entities;
using feedbackFlowAPI.Helpers;
using feedbackFlowAPI.Mappers.Implementations;
using feedbackFlowAPI.Mappers.Interface;
using feedbackFlowAPI.Services.Implementations;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.ObjectModel;

namespace UnitTests
{
    public class ClassStatistics : IClassFixture<PostgreSqlTestFixture>
    {
        private PostgreSqlTestFixture _fixture;
        private readonly ClassStatisticsDTO _result;

        public ClassStatistics(PostgreSqlTestFixture fixture)
        {
            _fixture = fixture;
            _result = InitializeAsync(fixture).GetAwaiter().GetResult();
        }

        private static async Task<ClassStatisticsDTO> InitializeAsync(PostgreSqlTestFixture fixture)
        {
            //Arrange
            await using var context = new FbfDbContext(fixture.DbContextOptions);

            Class mathClass = await context.Classes
                .Include(c => c.Students)
                .ThenInclude(s => s.StudentResults)
                .FirstAsync(c => c.Name == "Math2");

            QuestionSet questionSet = await context.QuestionSets
                .FirstAsync(q => q.Name == "Eksamenssaet 1");

            IClassMapper classMapper = new ClassMapper();
            ClassService service = new ClassService(context, classMapper);
            
            //Act
            return await service.GetStatistics(mathClass.Id, questionSet.Id);
        }

        //#region Date Ranges
        //[Fact]
        //public async Task QueryStartDate_ShouldBe_BeforeEndDate() =>
        //    _result.QueryStartDate.Should().BeBefore(_result.QueryEndDate);

        //[Fact]
        //public async Task QueryEndDate_ShouldBe_AfterStartDate() =>
        //    _result.QueryEndDate.Should().BeAfter(_result.QueryStartDate);

        //[Fact]
        //public async Task QueryEndDate_ShouldNotBe_InTheFuture() =>
        //    _result.QueryEndDate.Should().NotBeAfter(DateTime.UtcNow); 
        //#endregion

        #region ClassAvgScore
        [Fact]
        public async Task ClassAvgScoreCombined_ShouldBe_InValidRange() =>
            _result.ClassAvgScoreCombined.Should().BeInRange(0, 10);

        [Fact]
        public async Task ClassAvgScoreDigital() =>
            _result.ClassAvgScoreDigital.Should().BeInRange(0, 10);

        [Fact]
        public async Task ClassAvgScoreAnalog() =>
            _result.ClassAvgScoreAnalog.Should().BeInRange(0, 10);

        [Fact]
        public async Task ClassAvgScoreCombined_ShouldBe_BetweenAnalogAndDigital()
        {
            var min = Math.Min(_result.ClassAvgScoreDigital, _result.ClassAvgScoreAnalog);
            var max = Math.Max(_result.ClassAvgScoreDigital, _result.ClassAvgScoreAnalog);
            _result.ClassAvgScoreCombined.Should().BeInRange(min, max);
        }
        #endregion

        #region Lists
        [Fact]
        public async Task AllLists_ShouldNotBeNull()
        {
            _result.StudentsScoreAssignments.Should().NotBeNull();
            _result.ClassAvgSubjectScoreCombined.Should().NotBeNull();
            _result.ClassAvgSubjectScoreAnalog.Should().NotBeNull();
            _result.ClassAvgSubjectScoreDigital.Should().NotBeNull();
            _result.StudentsAvgScoreCombined.Should().NotBeNull();
            _result.StudentsAvgScoreAnalog.Should().NotBeNull();
            _result.StudentsAvgScoreDigital.Should().NotBeNull();
            _result.StudentsAvgScoreSubject.Should().NotBeNull();
            _result.WeakestSubjects.Should().NotBeNull();
        } 

        #region StudentScoreAssignments
        [Fact]
        public async Task StudentScoreAssignments_Items_ShouldHaveValidTypes() =>
            _result.StudentsScoreAssignments.Should().AllBeOfType<StudentsScore>();

        [Fact]
        public async Task StudentsScoreAssignments_Scores_ShouldBe_InValidRange() =>
            _result.StudentsScoreAssignments.Should().OnlyContain(s => s.Score >= 0 && s.Score <= 10);

        [Fact]
        public async Task StudentsScoreAssignments_ShouldHaveNo_NullUsers() =>
            _result.StudentsScoreAssignments.Should().OnlyContain(s => s.User != null);

        [Fact]
        public async Task StudentsScoreAssignments_ShouldHaveNo_NullQuestions() =>
            _result.StudentsScoreAssignments.Should().OnlyContain(s => s.Question != null);
        #endregion


        # region ClassAvgSubjectScore
        [Fact]
        public async Task ClassAvgSubjectScoreCombined_Items_ShouldHaveValidTypes() =>
            _result.ClassAvgSubjectScoreCombined.Should().AllBeOfType<SubjectScoreCombined>();

        [Fact]
        public async Task ClassAvgSubjectScoreDigital_Items_ShouldHaveValidTypes() =>
            _result.ClassAvgSubjectScoreDigital.Should().AllBeOfType<SubjectScore>();

        [Fact]
        public async Task ClassAvgSubjectScoreAnalog_Items_ShouldHaveValidTypes() =>
            _result.ClassAvgSubjectScoreAnalog.Should().AllBeOfType<SubjectScore>();

        [Fact]
        public async Task ClassAvgSubjectScoreCombined_Scores_ShouldBe_InValidRange() =>
            _result.ClassAvgSubjectScoreCombined.Should().OnlyContain(s => s.Score >= 0 && s.Score <= 10);

        [Fact]
        public async Task ClassAvgSubjectScoreDigital_Scores_ShouldBe_InValidRange() =>
            _result.ClassAvgSubjectScoreDigital.Should().OnlyContain(s => s.Score >= 0 && s.Score <= 10);

        [Fact]
        public async Task ClassAvgSubjectScoreAnalog_Scores_ShouldBe_InValidRange() =>
            _result.ClassAvgSubjectScoreAnalog.Should().OnlyContain(s => s.Score >= 0 && s.Score <= 10);

        [Fact]
        public async Task ClassAvgSubjectScoreCombined_ShouldHaveNo_NullSubjects() =>
            _result.ClassAvgSubjectScoreCombined.Should().OnlyContain(s => s.Subject != null);

        [Fact]
        public async Task ClassAvgSubjectScoreDigital_ShouldHaveNo_NullSubjects() =>
            _result.ClassAvgSubjectScoreDigital.Should().OnlyContain(s => s.Subject != null);

        [Fact]
        public async Task ClassAvgSubjectScoreAnalog_ShouldHaveNo_NullSubjects() =>
            _result.ClassAvgSubjectScoreAnalog.Should().OnlyContain(s => s.Subject != null);
        #endregion


        # region StudentsAvgScore
        public async Task StudentsAvgScoreCombined_Items_ShouldHaveValidTypes() =>
            _result.StudentsAvgScoreCombined.Should().AllBeOfType<StudentScore>();

        public async Task StudentsAvgScoreDigital_Items_ShouldHaveValidTypes() =>
            _result.StudentsAvgScoreDigital.Should().AllBeOfType<StudentScore>();

        public async Task StudentsAvgScoreAnalog_Items_ShouldHaveValidTypes() =>
            _result.StudentsAvgScoreAnalog.Should().AllBeOfType<StudentScore>();

        public async Task StudentsAvgScoreCombined_Scores_ShouldBe_InValidRange() =>
            _result.StudentsAvgScoreCombined.Should().OnlyContain(s => s.Score >= 0 && s.Score <= 10);

        public async Task StudentsAvgScoreDigital_Scores_ShouldBe_InValidRange() =>
            _result.StudentsAvgScoreDigital.Should().OnlyContain(s => s.Score >= 0 && s.Score <= 10);

        public async Task StudentsAvgScoreAnalog_Scores_ShouldBe_InValidRange() =>
            _result.StudentsAvgScoreAnalog.Should().OnlyContain(s => s.Score >= 0 && s.Score <= 10);
        #endregion


        #region StudentScoreSubject
        public async Task StudentsScoreSubject_Items_ShouldHaveValidTypes() =>
            _result.StudentsAvgScoreSubject.Should().AllBeOfType<StudentsAvgScoreSubject>();

        public async Task StudentsScoreSubject_Scores_ShouldBe_InValidRange() =>
            _result.StudentsAvgScoreSubject.Should()
                .OnlyContain(s => s.Score >= 0 && s.Score <= 10);

        public async Task StudentsScoreSubject_ShouldHaveNo_NullUsersOrSubjects() =>
            _result.StudentsAvgScoreSubject.Should()
                .OnlyContain(s => s.User != null && s.Subject != null);
        #endregion


        #region WeakestSubject
        public async Task WeakestSubjects_Items_ShouldHaveValidTypes() =>
            _result.WeakestSubjects.Should().AllBeOfType<WeakSubject>();

        public async Task WeakestSubjects_Scores_ShouldBe_InValidRange() =>
            _result.WeakestSubjects.Should().OnlyContain(w => w.Score >= 0 && w.Score <= 10);

        public async Task WeakestSubjects_ShouldHaveNo_NullSubjects() =>
            _result.WeakestSubjects.Should().OnlyContain(w => w.Subject != null);

        public async Task WeakestSubjects_ShouldBe_SortedAscending() => //Weakest score first
            _result.WeakestSubjects.Should().BeInAscendingOrder(w => w.Score);

        public async Task WeakestSubjects_ShouldNotHaveDuplicateSubjects() =>
            _result.WeakestSubjects.Select(w => w.Subject.Id).Should().OnlyHaveUniqueItems();

        public async Task WeakestSubjects_Scores_ShouldBe_AtOrBelowClassAverage() =>
            _result.WeakestSubjects.Should().OnlyContain(w => w.Score <= _result.ClassAvgScoreCombined);
        #endregion
        #endregion
    }
}
