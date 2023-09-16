using System.ComponentModel.DataAnnotations;

namespace MagazineApp.Service.DTOs.Stores;
public class StoreCreationDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Address { get; set; }
}
