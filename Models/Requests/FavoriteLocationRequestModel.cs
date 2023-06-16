using System.ComponentModel.DataAnnotations;

namespace Mappy.Models.Requests;

public class FavoriteLocationRequestModel
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public string Name { get; set; }
    
    [Required]
    public Coordinates Coordinates { get; set; }
}