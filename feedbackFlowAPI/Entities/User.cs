using feedbackFlowAPI.Helpers;
using System;
using System.Collections.Generic;

namespace feedbackFlowAPI.Entities;

public class User
{
    public int Id { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTimeOffset? DeletedAt { get; set; }

    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    public ICollection<Question>? Questions { get; set; } = new List<Question>();

    public ICollection<QuestionSet>? QuestionSets { get; set; } = new List<QuestionSet>();

    public ICollection<StudentResult>? StudentResults { get; set; } = new List<StudentResult>();

    public ICollection<StudentResult>? TeacherStudentResults { get; set; } = new List<StudentResult>();

    public ICollection<Class> TeacherClasses { get; set; } = new List<Class>();

    public ICollection<Class> StudentClasses { get; set; } = new List<Class>();
}
