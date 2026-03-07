using feedbackFlowAPI.Helpers;
using System;
using System.Collections.Generic;

namespace feedbackFlowAPI.Entities;

public class Question
{
    public int Id { get; set; }

    public string Points { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public QuestionType QuestionType { get; set; }

    public ClassLevel ClassLevel { get; set; }

    public int? UserId { get; set; }

    public int? CourseId { get; set; }

    public int? QuestionId { get; set; }

    public virtual ICollection<Appendix>? Appendices { get; set; } = new List<Appendix>();

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<Question> InverseQuestionNavigation { get; set; } = new List<Question>();

    public virtual Question? QuestionNavigation { get; set; }

    public virtual ICollection<QuestionAnswer>? QuestionAnswers { get; set; } = new List<QuestionAnswer>();

    public virtual ICollection<QuestionCollection>? QuestionCollections { get; set; } = new List<QuestionCollection>();

    public virtual ICollection<StudentResult>? StudentResults { get; set; } = new List<StudentResult>();

    public virtual User User { get; set; } = null!;

    public virtual ICollection<QuestionSet>? QuestionSets { get; set; } = new List<QuestionSet>();

    public virtual ICollection<Subject>? Subjects { get; set; } = new List<Subject>();

    public virtual ICollection<ContentType>? ContentTypes { get; set; } = new List<ContentType>();
}
