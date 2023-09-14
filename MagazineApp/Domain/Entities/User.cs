using MagazineApp.Domain.Commons;
using System.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace MagazineApp.Domain.Entities;
public class User : Auditable
{
    [MinLength(3), MaxLength(50)]
    public string FirstName { get; set; }
    [MinLength(3), MaxLength(50)]
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
