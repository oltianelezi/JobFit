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

    [HttpGet("{id}")]
    public IActionResult GetJobById(int id)
    {
        var job = _jobRepository.getJobById(id);
        if (job == null)
            return NotFound(new { message = "Job not found" });

        return Ok(job);
    }

    [HttpPut("update")]
    public IActionResult updateJob([FromBody] UpdateJobRequest request)
    {
        var job = new Job
        {
            Position = request.position,
            Company = request.company,
            Location = request.joblocation,
            Jobstatus = request.jobstatus,
            Jobtype = request.jobtype,
            JobId = request.jobId
        };

        _jobRepository.UpdateJob(job);

        return Ok();
    }

    [HttpPost("addJob")]
    public IActionResult AddJob([FromBody] AddJobRequest request)
    {
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

    [HttpDelete("{id}")]
    public IActionResult DeleteJob(int id)
    {
        _jobRepository.DeleteJob(id);

        return Ok();
    }

    [HttpGet("getJobsAppliedTo/{UserId}")]
    public IActionResult GetJobsAppliedTo(int UserId)
    {
        var jobs = _jobRepository.GetJobsAppliedTo(UserId);

        return Ok(jobs);
    }

}
