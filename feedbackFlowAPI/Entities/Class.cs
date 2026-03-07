using feedbackFlowAPI.Helpers;
using System;
using System.Collections.Generic;

namespace feedbackFlowAPI.Entities;

public class Class
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime Year { get; set; }

    public ClassLevel ClassLevel { get; set; }

    public bool IsDeleted { get; set; }

    public int CourseId { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<User>? Users { get; set; } = new List<User>();
}
