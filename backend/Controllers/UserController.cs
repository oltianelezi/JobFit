using System;
using Microsoft.AspNetCore.Mvc;
using backend.Repositories;
using BCrypt.Net;
using backend.Models;
using backend.DTOs.User;


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



    [HttpPost("signup")]
    public IActionResult Signup([FromBody] SignupRequest request)
    {
        if (request.username == null || request.password == null)
        {
            return BadRequest(new { message = "Empty fields" });
        }

        if (_userRepository.UsernameTaken(request.username))
        {
            return BadRequest(new { message = "Username is already taken." });

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
            YearsOfExp = request.yearsofexp
        };


        _userRepository.AddUser(user);

        return Ok();
    }


    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var user = _userRepository.GetUserByUsername(request.Username);
        if (user == null)
        {
            return Unauthorized(new { message = "Invalid username or password" });
        }

        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
        if (!isPasswordValid)
        {
            return Unauthorized(new { message = "Invalid username or password" });
        }

        return Ok(new { message = "Login successful", userId = user.UserId });
    }

    [HttpGet("{id}")]
    public IActionResult GetUserById(int id)
    {
        var user = _userRepository.GetUserById(id);

        if (user == null)
        {
            return NotFound(new { message = "User not found" });
        }

        return Ok(new
        {
            firstname = user.FirstName,
            lastname = user.LastName,
            email = user.Email,
            phonenumber = user.PhoneNumber,
            currjobtitle = user.CurrJobTitle,
            industry = user.Industry,
            yearsofexp = user.YearsOfExp,
            educationalbg = user.EducationalBg,
            desiredjob = user.DesiredJob,
            type = user.Type,
            cv = user.Cv
        });
    }


    [HttpPut("update")]
    public IActionResult UpdateUser([FromBody] UpdateRequest request)
    {
        var user = new User
        {
            PhoneNumber = request.phonenumber,
            FirstName = request.firstname,
            LastName = request.lastname,
            Email = request.email,
            CurrJobTitle = request.currjobtitle,
            Industry = request.industry,
            EducationalBg = request.educationalbg,
            DesiredJob = request.desiredjob,
            Cv = request.cv,
            YearsOfExp = request.yearsofexp,
            UserId = request.userId
        };

        _userRepository.updateUser(user);

        return Ok();
    }

    [HttpGet("getEmployerInfo/{jobId}")]
    public IActionResult GetEmployerInfo(int JobId)
    {
        var user = _userRepository.GetEmployerInfo(JobId);
        if (user != null)
            return Ok(user);
        else
            return NotFound();
    }

}
