using System.ComponentModel.DataAnnotations;

namespace Mappy.Models.Responses;

public class UserReponse
{
    [Required]
    public string UserName { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    
}