using Mappy.Configurations.Models;
using Mappy.Helpers;
using Mappy.Models;
using Mappy.Models.Requests;
using Mappy.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;

namespace Mappy.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly UserService _userService;
    private readonly JwtConfigurationModel _jwtBearerTokenSettings;

    public AuthenticationController(IOptions<JwtConfigurationModel> jwtTokenOptions,
        IConfiguration configuration, UserService userService)
    {
        _configuration = configuration;
        _userService = userService;
        _jwtBearerTokenSettings = jwtTokenOptions.Value;
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserModel userModel)
    {
        if (!ModelState.IsValid)
        {
            return new BadRequestObjectResult(new { Message = "User Registration Failed" });
        }

        if (!userModel.Password.Equals(userModel.ConfirmPassword))
        {
            return new BadRequestObjectResult(new { Message = "Passwords do not match!" });
        }

        if (!_userService.CreateUser(userModel))
        {
            return new BadRequestObjectResult(new { Message = "User Registration Failed" });
        }

        return Ok(new { Message = "User Registration Successful" });
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserModel userModel)
    {
        if (!ModelState.IsValid  || !_userService.DoesUserExist(userModel))
        {
            return new BadRequestObjectResult(new { Message = "Login failed" });
        }

        var token = AuthenticationHelper.GenerateToken(userModel, _jwtBearerTokenSettings);

        return Ok(new { Token = token, Message = "Success" });
    }
}