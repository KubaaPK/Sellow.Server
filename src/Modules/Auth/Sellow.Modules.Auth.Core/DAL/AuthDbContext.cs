using Microsoft.EntityFrameworkCore;
using Sellow.Modules.Auth.Core.Entities;

namespace Sellow.Modules.Auth.Core.DAL;

internal sealed class AuthDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;

    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        modelBuilder.HasDefaultSchema("Auth");
    }
}