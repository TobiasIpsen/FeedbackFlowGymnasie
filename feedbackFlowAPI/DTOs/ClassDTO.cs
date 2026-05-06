using feedbackFlowAPI.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace feedbackFlowAPI.DTOs;

public partial class ClassDTO
{
    public string Name { get; set; } = null!;

    public DateTimeOffset Year { get; set; }

    public Education Education { get; set; }

    public ClassLevel ClassLevel { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }

    public int CourseId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "TeacherId is required.")]
    public int TeacherId { get; set; }
}
