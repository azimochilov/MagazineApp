using MagazineApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

namespace MagazineApp.Data.Contexts;
public class AppDbContext : DbContext
{
    //public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Store> Stores { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string connectionString = "Server=localhost;Database=mydatabase;User=root;Password=Yaironman@.26;Port=3306;";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }
}
