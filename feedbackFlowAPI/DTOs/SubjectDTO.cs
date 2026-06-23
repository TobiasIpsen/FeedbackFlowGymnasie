using System;
using System.Collections.Generic;

namespace feedbackFlowAPI.DTOs;

public partial class SubjectDTO
{
    /// <summary>
    /// trigonometri, vectors
    /// </summary>
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public SubjectDTO()
    { }

    public SubjectDTO(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
