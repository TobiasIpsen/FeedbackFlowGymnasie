using System;
using System.Collections.Generic;

namespace feedbackFlowAPI.DTOs;

/// <summary>
/// (Billag)
/// </summary>
public partial class Appendix
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Url { get; set; } = null!;

    public bool Isdeleted { get; set; }
}
