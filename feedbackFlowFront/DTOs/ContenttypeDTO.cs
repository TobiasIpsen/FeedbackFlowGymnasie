using System;
using System.Collections.Generic;

namespace feedbackFlowAPI.DTOs;

/// <summary>
/// (diagram, tekst)
/// </summary>
public partial class ContenttypeDTO
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool Isdeleted { get; set; }
}
