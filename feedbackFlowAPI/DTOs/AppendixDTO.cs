using System;
using System.Collections.Generic;

namespace feedbackFlowAPI.DTOs;

/// <summary>
/// (Billag)
/// </summary>
public partial class AppendixDTO
{
    public string Name { get; set; } = null!;

    public string Url { get; set; } = null!;

    public DateTimeOffset IsDeleted { get; set; }
}
