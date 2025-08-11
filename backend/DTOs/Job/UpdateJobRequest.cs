using System;

namespace backend.DTOs.Job;

public class UpdateJobRequest
{
    public string position { get; set; } = string.Empty;
    public string company { get; set; } = string.Empty;
    public string joblocation { get; set; } = string.Empty;
    public string jobstatus { get; set; } = string.Empty;
    public string jobtype { get; set; } = string.Empty;
    public int jobId { get; set; }

}
