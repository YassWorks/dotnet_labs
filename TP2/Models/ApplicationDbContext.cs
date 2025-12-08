using Microsoft.EntityFrameworkCore;

namespace TP2.Models;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<MembershipType> MembershipTypes { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }
}
