using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Mappy.Models.Responses;
using Mappy.Services;
using Microsoft.AspNetCore.Authorization;
using Mappy.Models;
using Mappy.Models.Requests;

namespace Mappy.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class LocationController : ControllerBase
{
  //=============================================================================================
  private readonly LocationService _locationService;


  //=============================================================================================
  public LocationController(LocationService locationService)
  {
      _locationService = locationService;
  }



  //=============================================================================================
  [HttpGet("")]
  public async Task<IActionResult> GetLocationById()
  {
    var id = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.UserData)?.Value;

    if (id == null)
    {
      return BadRequest(new { message = "Cannot get the Id of the user..." });
    }

    var result = await _locationService.GetLocationById(id);

    if (!result.IsSuccessful)
    {
      return NotFound(new { message = result.Message });
    }
    else
    {
      return Ok(result.Data);
    }
  }


  //=============================================================================================
  [HttpGet("last")]
  public async Task<IActionResult> GetLastLocationByUser()
  {
    var id = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.UserData)?.Value;

    if (id == null)
    {
      return BadRequest(new { message = "Cannot get the Id of the user..." });
    }

    var result = await _locationService.GetLastLocationByUser(id);

    if (!result.IsSuccessful)
    {
      return NotFound(new { message = result.Message });
    }
    else
    {
      return Ok(result.Data);
    }
  }


  //=============================================================================================
  [HttpPost("")]
  public async Task<IActionResult> AddLocation(Coordinates coordinates)
  {
    var id = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.UserData)?.Value;

    if (id == null)
    {
      return BadRequest(new { message = "Cannot get the Id of the user..." });
    }

    var result = await _locationService.AddLocation(new LocationRequestModel
    {
      UserId = new Guid(id),
      Coordinates = coordinates
    });

    if (!result.IsSuccessful)
    {
      return BadRequest(new { message = result.Message });
    }
    else
    {
      return Ok(result.Data);
    }
  }
}