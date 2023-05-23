using Mappy.Configurations.Models;
using Mappy.Helpers;
using Mappy.Models;
using Mappy.Models.Requests;
using Mappy.Models.Responses;
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
  //=============================================================================================
  private readonly IConfiguration _configuration;
  private readonly UserService _userService;
  private readonly JwtConfigurationModel _jwtBearerTokenSettings;


  //=============================================================================================
  public AuthenticationController(IOptions<JwtConfigurationModel> jwtTokenOptions,
      IConfiguration configuration, UserService userService)
  {
      _configuration = configuration;
      _userService = userService;
      _jwtBearerTokenSettings = jwtTokenOptions.Value;
  }



  //=============================================================================================
  [HttpPost("register")]
  public async Task<IActionResult> Register([FromBody] RegisterUserModel model)
  {
      if (!model.Password.Equals(model.ConfirmPassword))
      {
          return new BadRequestObjectResult(new { Message = "Passwords do not match!" });
      }

      var result = await _userService.RegisterUser(model);

      if (!result.IsSuccessful)
      {
          return new BadRequestObjectResult(new { Message = result.Message });
      }

      return Ok(new { Message = "User Registration Successful" });
  }


  //=============================================================================================
  [HttpPost("login")]
  public async Task<IActionResult> Login([FromBody] LoginUserModel model)
  {
      var result = await _userService.LoginUser(model);

      if (!result.IsSuccessful)
      {
          return new BadRequestObjectResult(new { Message = result.Message });
      }

      var user = result.Data as SecureUserModel;

      if (user != null)
      {
          var token = AuthenticationHelper.GenerateToken(user, _jwtBearerTokenSettings);

          return Ok(new { Token = token, Message = "Success", User = user });
      }

      return new BadRequestObjectResult(new { Message = "Something went wrong..." });
  }
}