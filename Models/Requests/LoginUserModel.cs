using System.ComponentModel.DataAnnotations;

namespace Mappy.Models.Requests;

public class LoginUserModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
}