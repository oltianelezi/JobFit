using backend.Models;
using backend.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("job")]
[ApiController]
public class JobController : ControllerBase
{
    private readonly JobRepository _jobRepository;

    public JobController()
    {
        _jobRepository = new JobRepository();
    }

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

    [HttpPost("addJob")]
    public IActionResult AddJob([FromBody] AddJobRequest request)
    {
        // if (request.userId == null)
        // {
        //     return BadRequest(new { message = "Invalid user ID" });
        // }

        var job = new Job
        {
            Position = request.position,
            Location = request.joblocation,
            Company = request.company,
            Jobstatus = request.jobstatus,
            Jobtype = request.jobtype,
            DatePosted = request.date,
            UserId = request.userId
        };

         _jobRepository.AddJob(job);

        return Ok();
    }
}
