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
[Route("favorite-location")]
public class FavoriteLocationController : ControllerBase
{
  //=============================================================================================
  private readonly FavoriteLocationService _favoriteLocationService;


  //=============================================================================================
  public FavoriteLocationController(FavoriteLocationService favoriteLocationService)
  {
    _favoriteLocationService = favoriteLocationService;
  }



  //=============================================================================================
  [HttpGet("")]
  public async Task<IActionResult> GetAllFavoriteLocationsById()
  {
    var id = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.UserData)?.Value;

    if (id == null)
    {
      return BadRequest(new { message = "Cannot get the Id of the user..." });
    }

    var result = await _favoriteLocationService.GetAllFavoriteLocationsById(id);

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
  public async Task<IActionResult> AddFavoriteLocation(SecureFavoriteLocationRequestModel location)
  {
    var id = User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.UserData)?.Value;

    if (id == null)
    {
      return BadRequest(new { message = "Cannot get the Id of the user..." });
    }

    var result = await _favoriteLocationService.AddFavoriteLocation(new FavoriteLocationRequestModel
    {
      UserId = new Guid(id),
      Name = location.Name,
      Coordinates = location.Coordinates
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


  //=============================================================================================
  [HttpDelete("")]
  public async Task<IActionResult> DeleteFavoriteLocationsById(Guid id)
  {
    var result = await _favoriteLocationService.DeleteFavoriteLocation(id);

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