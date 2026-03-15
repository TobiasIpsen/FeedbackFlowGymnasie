using feedbackFlowAPI.Helpers;
using System;
using System.Collections.Generic;

namespace feedbackFlowAPI.Entities;

public class Question
{
    public int Id { get; set; }

    public string ImgSrc { get; set; } = null!;

    public string Points { get; set; } = null!;

    public DateTimeOffset? DeletedAt { get; set; }

    public ExamType ExamType { get; set; }

    public ClassLevel ClassLevel { get; set; }

    public QuestionDifficulty QuestionDifficulty { get; set; }

    public QuestionMethodRequirement QuestionMethodRequirement { get; set; }

    public Education Education { get; set; }

    public QuestionContext QuestionContext { get; set; }

    public StandardQuestion StandardQuestion { get; set; }

    public NewOldSystem NewOldSystem { get; set; }


    public ICollection<Appendix>? Appendices { get; set; } = new List<Appendix>();

    public int? CourseId { get; set; }

    public Course Course { get; set; } = null!;

    public ICollection<QuestionAnswer>? QuestionAnswers { get; set; } = new List<QuestionAnswer>();

    public ICollection<QuestionCollection>? QuestionCollections { get; set; } = new List<QuestionCollection>();

    public ICollection<StudentResult>? StudentResults { get; set; } = new List<StudentResult>();

    public int UserId { get; set; }

    public User User { get; set; } = null!;

    public ICollection<QuestionQuestionSet> QuestionSets { get; set; } = new List<QuestionQuestionSet>();

    public ICollection<Subject>? Subjects { get; set; } = new List<Subject>();

    public ICollection<ContentType>? ContentTypes { get; set; } = new List<ContentType>();
}
