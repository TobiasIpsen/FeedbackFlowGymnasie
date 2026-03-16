using System;
using System.Collections.Generic;

namespace feedbackFlowAPI.DTOs;

public partial class CourseDTO
{
    public int Id { get; set; }

    /// <summary>
    /// mat, fys, etc.
    /// </summary>
    public string Name { get; set; } = null!;
}
