using feedbackFlowAPI.Entities;
using System;
using System.Collections.Generic;

namespace feedbackFlowAPI.DTOs;

public partial class QuestionAnswerDTO
{
    public string? Name { get; set; }

    public string? Url { get; set; }

    public IFormFile? File { get; set; } // Til upload af fil (ikke bruges i databasen så er kun i DTO)

    public DateTimeOffset? DeletedAt { get; set; }

    public int QuestionId { get; set; }



    public int QuestionSetId { get; set; }

    public Question Question { get; set; } = null!;

    public QuestionSet? QuestionSet { get; set; } = null!;

    public ICollection<ContentType> ContentTypes { get; set; } = new List<ContentType>();
}
