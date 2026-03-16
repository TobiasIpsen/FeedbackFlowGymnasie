using System;
using System.Collections.Generic;

namespace feedbackFlowAPI.Entities;

/// <summary>
/// Mat, Fys, etc.
/// </summary>
public class Course
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public ICollection<Class> Classes { get; set; } = new List<Class>();

    public ICollection<Question> Questions { get; set; } = new List<Question>();
}
