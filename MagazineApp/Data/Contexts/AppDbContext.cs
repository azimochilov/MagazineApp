using MagazineApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

namespace MagazineApp.Data.Contexts;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Store> Stores { get; set; }
}
