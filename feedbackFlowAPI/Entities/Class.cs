using feedbackFlowAPI.Helpers;
using System;
using System.Collections.Generic;

namespace feedbackFlowAPI.Entities;

public class Class
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTimeOffset Year { get; set; }

    public Education Education { get; set; }

    public ClassLevel ClassLevel { get; set; }

    public DateTimeOffset IsDeleted { get; set; }

    public int CourseId { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<User>? Users { get; set; } = new List<User>();
}
