using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class TheaterDbContext : DbContext
{
    public DbSet<Spectacle> Spectacles { get; set; } = null!;
    public DbSet<Ticket> Tickets { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;

    public TheaterDbContext(DbContextOptions<TheaterDbContext> options) : base(options)
    {
    }
}
