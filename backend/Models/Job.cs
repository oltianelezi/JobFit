using System;

namespace backend.Models;

public class Job
{
    public int JobId { get; set; }
    public string Position { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public string Jobstatus { get; set; } = string.Empty;
    public string Jobtype { get; set; } = string.Empty;
    public DateOnly DatePosted { get; set; }
    public int UserId { get; set; }
}
