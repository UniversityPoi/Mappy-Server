using System.ComponentModel.DataAnnotations;

namespace Mappy.Models.Requests;

public class LocationRequestModel
{
    [Required]
    public Guid UserId { get; set; }
    
    [Required]
    public Coordinates Coordinates { get; set; }
}