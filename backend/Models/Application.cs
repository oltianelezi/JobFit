using System;

namespace backend.Models;

public class Application
{
    public int UserId { get; set; }
    public int JobId { get; set; }
    public DateOnly InterviewDate { get; set; }
}
