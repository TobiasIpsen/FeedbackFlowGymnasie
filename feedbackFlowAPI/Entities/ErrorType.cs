using System;
using System.Collections.Generic;

namespace feedbackFlowAPI.Entities;

public class ErrorType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? UserId { get; set; }

    public User User { get; set; } = null!;

    public ICollection<StudentResult>? StudentResults { get; set; } = new List<StudentResult>();
}
