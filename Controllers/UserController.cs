using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Mappy.Models.Responses;
using Mappy.Services;
using Microsoft.AspNetCore.Authorization;

namespace Mappy.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
  //=============================================================================================
  private readonly UserService _userService;


  //=============================================================================================
  public UserController(UserService userService)
  {
      _userService = userService;
  }



  //=============================================================================================
  [HttpGet("{id}")]
  public async Task<IActionResult> GetUserById(Guid id)
  {
    var result = await _userService.GetUserById(id.ToString());

    if (!result.IsSuccessful)
    {
      return NotFound(new { message = result.Message });
    }
    else
    {
      return Ok(result.Data);
    }
  }
}