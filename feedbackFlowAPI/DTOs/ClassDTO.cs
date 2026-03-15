using System;
using System.Collections.Generic;

namespace feedbackFlowAPI.DTOs;

public partial class ClassDTO
{
    public string Name { get; set; } = null!;

    public DateTimeOffset Year { get; set; }

    public DateTimeOffset IsDeleted { get; set; }
}
