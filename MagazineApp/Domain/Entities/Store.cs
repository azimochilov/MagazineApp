using MagazineApp.Domain.Commons;

namespace MagazineApp.Domain.Entities;
public class Store : Auditable
{
    public string Name { get; set; }
    public string Address { get; set; }
}
