using System;
using System.Collections.Generic;

namespace feedbackFlowAPI.DTOs;

public partial class UserDTO
{
    public int Id { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Email { get; set; } = null!;
}
