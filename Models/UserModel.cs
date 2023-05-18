using System.ComponentModel.DataAnnotations;

namespace Mappy.Models;

public class UserModel
{
    public Guid Id { get; set; }
    
    [Required]
    public string UserName { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
}