using feedbackFlowAPI.Helpers;

namespace feedbackFlowAPI.DTOs.ClassStatistics
{



    public class ClassStatisticsDTO
    {
        //public DateTimeOffset QueryStartDate { get; set; }
        //public DateTimeOffset QueryEndDate { get; set; }

        public List<StudentsScore> StudentsScoreAssignments { get; set; } = new List<StudentsScore>();

        public double ClassAvgScoreCombined { get; set; }
        public double ClassAvgScoreAnalog { get; set; }
        public double ClassAvgScoreDigital { get; set; }

        public List<SubjectScoreCombined> ClassAvgSubjectScoreCombined { get; set; } = new List<SubjectScoreCombined>();
        public List<SubjectScore> ClassAvgSubjectScoreAnalog { get; set; } = new List<SubjectScore>();
        public List<SubjectScore> ClassAvgSubjectScoreDigital { get; set; } = new List<SubjectScore>();

        public List<StudentScore> StudentsAvgScoreCombined { get; set; } = new List<StudentScore>();
        public List<StudentScore> StudentsAvgScoreAnalog { get; set; } = new List<StudentScore>();
        public List<StudentScore> StudentsAvgScoreDigital { get; set; } = new List<StudentScore>();

        public List<StudentsAvgScoreSubject> StudentsAvgScoreSubject { get; set; } = new List<StudentsAvgScoreSubject>();
        public List<WeakSubject> WeakestSubjects { get; set; } = new List<WeakSubject>();


    }
}
