using System.ComponentModel.DataAnnotations;

namespace Mappy.Models.Requests
{
  public class SecureFavoriteLocationRequestModel
  {
    [Required]
    [MinLength(3)]
    [MaxLength(20)]
    public string Name { get; set; }

    [Required]
    public Coordinates Coordinates { get; set; }
  }
}