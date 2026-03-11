using System;
using System.Collections.Generic;

namespace feedbackFlowAPI.DTOs;

public partial class ClassDTO
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime Year { get; set; }

    public bool Isdeleted { get; set; }
}
