using backend.DTOs.Application;
using backend.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("application")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {

        private readonly ApplicationRepository _applicationRepository;

        public ApplicationController()
        {
            _applicationRepository = new ApplicationRepository();
        }

        [HttpPost("apply")]
        public IActionResult Apply([FromBody] ApplicationRequest request)
        {
            _applicationRepository.Apply(request.UserId, request.JobId);

            return Ok();
        }

        [HttpPost("applyCheck")]
        public IActionResult ApplyCheck([FromBody] ApplicationRequest request)
        {
            var check = _applicationRepository.ApplyCheck(request.UserId, request.JobId);

            return Ok(new { applied = check });
        }

        [HttpGet("applicants/{JobId}")]
        public IActionResult GetApplicants(int JobId)
        {
            var Applicants = _applicationRepository.GetApplicants(JobId);

            return Ok(new { applicants = Applicants });
        }
    }
}
