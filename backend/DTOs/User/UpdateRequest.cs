using System;

namespace backend.DTOs.User;

public class UpdateRequest
{
    public string firstname { get; set; } = string.Empty;
    public string lastname { get; set; } = string.Empty;
    public string email { get; set; } = string.Empty;
    public string currjobtitle { get; set; } = string.Empty;
    public string industry { get; set; } = string.Empty;
    public string educationalbg { get; set; } = string.Empty;
    public string desiredjob { get; set; } = string.Empty;
    public string cv { get; set; } = string.Empty;
    public string phonenumber { get; set; } = string.Empty;
    public int yearsofexp { get; set; }
    public int userId { get; set; }
}
