using System.ComponentModel.DataAnnotations;

namespace MagazineApp.Service.DTOs.Users;
public class UserUpdateDto
{
    [Required]
    public long Id { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
    [Required]
    public string Email { get; set; }
}
