using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Mappy.Models;
using Mappy.Services;
using Microsoft.AspNetCore.Authorization;

namespace Mappy.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet("username")]
    public IActionResult GetUsername()
    {
        var email = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;

        Console.WriteLine(email);

        return Ok();
    }
}