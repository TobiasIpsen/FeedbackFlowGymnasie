using System.Collections.Generic;

namespace feedbackFlowAPI.DTOs;

public partial class StudentResultMistakesDTO
{
    public ICollection<int> MistakeIds { get; set; } = new List<int>();
}