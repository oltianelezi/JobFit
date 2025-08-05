using System;
using Microsoft.AspNetCore.Mvc;
using backend.Repositories;
using BCrypt.Net;
using backend.Models;


namespace backend.Controllers;

[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
    private readonly UserRepository _userRepository;

    public UserController()
    {
        _userRepository = new UserRepository();
    }
    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok();
    }

    public class SignupRequest
    {
        public string firstname { get; set; } = string.Empty;
        public string lastname { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string type { get; set; } = string.Empty;
        public string currjobtitle { get; set; } = string.Empty;
        public string industry { get; set; } = string.Empty;
        public string educationalbg { get; set; } = string.Empty;
        public string desiredjob { get; set; } = string.Empty;
        public string username { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string confirmpassword { get; set; } = string.Empty;
        public string cv { get; set; } = string.Empty;
        public string phonenumber { get; set; } = string.Empty;
        public string yearsofexp { get; set; } = string.Empty;
    }

    [HttpPost("signup")]
    public IActionResult Signup([FromBody] SignupRequest request)
    {
        if (request.username == null || request.password == null)
        {
            return BadRequest(new { message = "Empty fields" });
        }

        if (_userRepository.UsernameTaken(request.username) != null)
        {
            return BadRequest(new { message = "Username is already taken." });

        }

        int? parsedYearsOfExp = null;
        if (!string.IsNullOrEmpty(request.yearsofexp))
        {
            if (int.TryParse(request.yearsofexp, out var years))
            {
                parsedYearsOfExp = years;
            }
        }
        
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.password);
        var user = new User
        {
            Username = request.username,
            Password = passwordHash,
            PhoneNumber = request.phonenumber,
            FirstName = request.firstname,
            LastName = request.lastname,
            Email = request.email,
            Type = request.type,
            CurrJobTitle = request.currjobtitle,
            Industry = request.industry,
            EducationalBg = request.educationalbg,
            DesiredJob = request.desiredjob,
            Cv = request.cv,
            YearsOfExp = parsedYearsOfExp
        };


        _userRepository.AddUser(user);

        return Ok();
    }
}
