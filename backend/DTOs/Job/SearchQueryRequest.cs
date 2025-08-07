using System;

namespace backend.DTOs.Job;

public class SearchQueryRequest
{
    public string search { get; set; } = string.Empty;
    public string jobstatus { get; set; } = string.Empty;
    public string jobtype { get; set; } = string.Empty;
    public int userId { get; set; }
    public string userType { get; set; } = string.Empty;

}
