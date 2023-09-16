using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace MagazineApp.Service.DTOs.Users;
public class UserCreationDto
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }    
    
}
