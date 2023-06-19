using System.ComponentModel.DataAnnotations;

namespace Mappy.Models.Requests
{
  public class SecureFavoriteLocationRequestModel
  {
    [Required]
    public string Name { get; set; }

    [Required]
    public Coordinates Coordinates { get; set; }
  }
}