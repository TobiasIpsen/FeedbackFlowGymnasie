using feedbackFlowAPI.Entities;
using System;
using System.Collections.Generic;

namespace feedbackFlowAPI.DTOs;

public partial class UserDTO
{
    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public UserDTO(User entity)
    {
        this.Firstname = entity.Firstname;
        this.Lastname = entity.Lastname;
        this.Email = entity.Email;
    }

}
