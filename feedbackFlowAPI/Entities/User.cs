using feedbackFlowAPI.DTOs;
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

    public bool IsDeleted { get; set; }

    public UserRole UserRole { get; set; } = UserRole.Student;

    public virtual ICollection<ErrorType>? ErrorTypes { get; set; } = new List<ErrorType>();

    public virtual ICollection<Question>? Questions { get; set; } = new List<Question>();

    public virtual ICollection<StudentResult>? StudentResults { get; set; } = new List<StudentResult>();

    public virtual ICollection<Class>? Classes { get; set; } = new List<Class>();
}
