using System.ComponentModel.DataAnnotations;

namespace Mappy.Models.Requests;

public class FavoriteLocationRequestModel : SecureFavoriteLocationRequestModel
{
    [Required]
    public Guid UserId { get; set; }
}