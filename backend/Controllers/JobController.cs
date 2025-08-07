using backend.Models;
using backend.DTOs;
using backend.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using backend.DTOs.Job;

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

    [HttpPost("getJobs")]
    public IActionResult getJobs([FromBody] SearchQueryRequest request)
    {
        var jobs = _jobRepository.getJobs(request);
        return Ok(jobs);
    }
}
