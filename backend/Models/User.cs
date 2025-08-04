using System;

namespace backend.Models;

public class User
{
    public int UserId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string CurrJobTitle { get; set; } = string.Empty;
    public string Industry { get; set; } = string.Empty;
    public string EducationalBg { get; set; } = string.Empty;
    public string DesiredJob { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public int YearsOfExp { get; set; }

}
