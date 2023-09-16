using System.ComponentModel.DataAnnotations;

namespace MagazineApp.Service.DTOs.Stores;
public class StoreUpdateDto
{
    [Required]
    public long Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Address { get; set; }
}
