using System;

namespace backend.DTOs;

public class AddJobRequest
{
    public string position { get; set; } = string.Empty;
    public string company { get; set; } = string.Empty;
    public string joblocation { get; set; } = string.Empty;
    public string jobstatus { get; set; } = string.Empty;
    public string jobtype { get; set; } = string.Empty;
    public DateOnly date { get; set; }
    public int userId { get; set; }
}
